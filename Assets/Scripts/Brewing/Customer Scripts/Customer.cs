using BrewingData;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public delegate void ServedEventHandler(Customer sender);
    public event ServedEventHandler CustomerServed;

    private Drinks _wantedDrink;
    private Order _wantedOrder;

    private Drinks _neededDrink;
    private Order _neededOrder;

    private Drinks _dislikedDrink;
    private Order _dislikedOrder;

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

    public void SetNewCustomer(Drinks wantedDrink, Drinks neededDrink, Drinks dislikedDrink)
    {
        SetDrinks(wantedDrink, neededDrink, dislikedDrink);
        _timer.ResetTime();
        _analysis.IsAlreadyAnalysed = false;
    }

    private void SetDrinks(Drinks wantedDrink, Drinks neededDrink, Drinks dislikedDrink)
    {
        _wantedDrink = wantedDrink;
        _neededDrink = neededDrink;
        _dislikedDrink = dislikedDrink;
        _wantedOrder = new Order(_wantedDrink, OrderTypes.Wanted);
        _neededOrder = new Order(_neededDrink, OrderTypes.Needed);
        _dislikedOrder = new Order(_dislikedDrink, OrderTypes.Disliked);

        _wantedText.Drink = _wantedDrink;
        _neededText.Drink = _neededDrink;

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
        if (_wantedOrder.MatchDrink(contents))
        {
            print("wanted");
            return _wantedOrder;
        }
        if (_neededOrder.MatchDrink(contents))
        {
            print("needed");
            return _neededOrder;
        }
        if (_dislikedOrder.MatchDrink(contents))
        {
            print("disliked");
            return _dislikedOrder;
        }
        print("none");
        return Order.EmptyOrder;
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