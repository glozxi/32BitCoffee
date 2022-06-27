using BrewingData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject _imageObject;
    [SerializeField]
    private GameObject _mainImage;
    [SerializeField]
    private GameObject _expressionImage;

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

    private void OnEnable()
    {
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

        _mainImage.GetComponent<Image>().sprite = _data.MainSprite;
        _expressionImage.GetComponent<Image>().sprite = _data.Expression;

        _isStoryAffected = data.IsStoryAffected;
        _timer.ResetTime();
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
        Points.AddCash(CheckDrink(cup.Contents), _timer);
        cup.ResetCup();
        CustomerServed?.Invoke(this, _outcome, _outcomeDrink);
    }

    private Order CheckDrink(List<Ingredients> contents)
    {
        Order order;
        if (_wantedOrder.DoesDrinkMatch(contents))
        {
            print("wanted");
            _outcome = 0;
            _outcomeDrink = _data.Wanted;
            order =  _wantedOrder;
        }
        else if (_neededOrder.DoesDrinkMatch(contents))
        {
            print("needed");
            _outcome = 1;
            _outcomeDrink = _data.Needed;
            order = _neededOrder;
        }
        else if (_dislikedOrder.DoesDrinkMatch(contents))
        {
            print("disliked");
            _outcome = -1;
            _outcomeDrink = _data.Disliked;
            order = _dislikedOrder;
        }
        else
        {
            print("none");
            _outcome = 2;
            _outcomeDrink = Drinks.None;
            order = Order.EmptyOrder;
        }
        return order;

    }

    public void SetObjectActive(bool value)
    {
        _imageObject?.SetActive(value);
        gameObject.SetActive(value);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}