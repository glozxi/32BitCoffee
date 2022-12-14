using BrewingData;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Customer : MonoBehaviour
{
    public delegate void ServedEventHandler(Customer sender, int outcome, Drinks drink);
    public event ServedEventHandler CustomerServed;

    private CustomerData _data;

    private Order _wantedOrder;

    private Order _neededOrder;

    private Order _dislikedOrder;

    private bool _isStoryAffected;
    public bool IsStoryAffected
    { get => _isStoryAffected; }

    private int _outcome;
    private Drinks _outcomeDrink;

    [SerializeField]
    private WantedOrderText _wantedText;
    [SerializeField]
    private NeededOrderText _neededText;

    [SerializeField]
    private Servebox _servebox;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private AnalyseOrder _analysis;

    [SerializeField]
    private string pos;
    private string _prevName;

    private List<CustomerTimeUpgrade> _timeUpgrades;

    private void OnEnable()
    {
        _timeUpgrades = State.Instance.Upgrades.OfType<CustomerTimeUpgrade>().ToList();
        _servebox.CupCollision += OnServed;
    }

    private void OnDisable()
    {
        _servebox.CupCollision -= OnServed;
    }

    public void SetNewCustomer(CustomerData data)
    {
        _data = data;

        SetDrinks();
        SetText();

        CharacterManager.instance.CMDisableCharBrew(_prevName);
        CharacterManager.instance.CMCharBrew(_data.Name, _data.MainSprite, _data.Expression, pos);
        _prevName = _data.Name;

        _isStoryAffected = data.IsStoryAffected;
        _timer.ResetTime();
        _timer.Duration = data.BonusTime;
        _timer.StartTime();
        foreach (var item in _timeUpgrades)
        {
            item.UseUpgrade(_timer);
        }
        _analysis.IsAlreadyAnalysed = false;
    }

    private void SetDrinks()
    {
        _wantedOrder = new Order(_data.Wanted, OrderTypes.Wanted);
        _neededOrder = new Order(_data.Needed, OrderTypes.Needed);
        _dislikedOrder = new Order(_data.Disliked, OrderTypes.Disliked);
    }

    private void SetText()
    {
        _wantedText.TextToDisplay = _data.OrderTalk;
        _neededText.TextToDisplay = _data.AnalyseInformation;
        _neededText.SetObjectInactive();
    }

    private void OnServed(Cup cup)
    {
        LevelCash currCash = FindObjectOfType<LevelCash>();
        currCash.AddCash(CheckDrink(cup.Contents), _timer, _data.BonusMult);
        cup.ResetCup();
        CustomerServed?.Invoke(this, _outcome, _outcomeDrink);
    }

    private Order CheckDrink(List<IngredientScriptableObject> contents)
    {
        Order order;
        if (_wantedOrder.DoesDrinkMatch(contents))
        {
            _outcome = 0;
            _outcomeDrink = _data.Wanted;
            order =  _wantedOrder;
        }
        else if (_neededOrder.DoesDrinkMatch(contents))
        {
            _outcome = 1;
            _outcomeDrink = _data.Needed;
            order = _neededOrder;
        }
        else if (_dislikedOrder.DoesDrinkMatch(contents))
        {
            _outcome = -1;
            _outcomeDrink = _data.Disliked;
            order = _dislikedOrder;
        }
        else
        {
            _outcome = 2;
            _outcomeDrink = Drinks.None;
            order = Order.EmptyOrder;
        }
        return order;

    }

    public void SetObjectActive(bool value)
    {
        if (!value) CharacterManager.instance.CMDisableCharBrew(_prevName);
        gameObject.SetActive(value);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}