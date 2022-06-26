using BrewingData;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public delegate void ServedEventHandler(Customer sender);
    public event ServedEventHandler CustomerServed;

    private CustomerData _data;

    private Order _wantedOrder;

    private Order _neededOrder;

    private Order _dislikedOrder;

    private bool _isStoryAffected;

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
        CustomerServed?.Invoke(this);
    }

    private Order CheckDrink(List<Ingredients> contents)
    {
        int outcome;
        Drinks drink;
        Order order;
        if (_wantedOrder.DoesDrinkMatch(contents))
        {
            print("wanted");
            outcome = 0;
            drink = _data.Wanted;
            order =  _wantedOrder;
        }
        else if (_neededOrder.DoesDrinkMatch(contents))
        {
            print("needed");
            outcome = 1;
            drink = _data.Needed;
            order = _neededOrder;
        }
        else if (_dislikedOrder.DoesDrinkMatch(contents))
        {
            print("disliked");
            outcome = -1;
            drink = _data.Disliked;
            order = _dislikedOrder;
        }
        else
        {
            print("none");
            outcome = 2;
            drink = Drinks.None;
            order = Order.EmptyOrder;
        }

        if (_isStoryAffected)
        {
            SetState(outcome, drink);
        }
        return order;

    }

    private void SetState(int outcome, Drinks drink)
    {
        State.Outcome = outcome;
        State.Drink = drink.ToString();
    }

    public void SetObjectActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}