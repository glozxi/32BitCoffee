using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;

public class Customer : MonoBehaviour
{
    public delegate void Served(Customer sender);
    public event Served CustomerServed;

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


    private void OnEnable()
    {
        _servebox.CupCollision += OnServed;
    }

    private void OnDisable()
    {
        _servebox.CupCollision -= OnServed;
    }

    public void SetCustomer(Drinks wantedDrink, Drinks neededDrink, Drinks dislikedDrink)
    {
        SetDrinks(wantedDrink, neededDrink, dislikedDrink);
    }

    private void SetDrinks(Drinks wantedDrink, Drinks neededDrink, Drinks dislikedDrink)
    {
        _wantedDrink = wantedDrink;
        _neededDrink = neededDrink;
        _dislikedDrink = dislikedDrink;
        _wantedOrder = new Order(_wantedDrink);
        _neededOrder = new Order(_neededDrink);
        _dislikedOrder = new Order(_dislikedDrink);

        _wantedText.Drink = _wantedDrink;
        _neededText.Drink = _neededDrink;

        _neededText.SetObjectInactive();
        
    }

    private void OnServed(Cup cup)
    {
        CheckDrink(cup.Contents);
        cup.ResetCup();
        CustomerServed?.Invoke(this);
    }

    private void CheckDrink(List<Ingredients> contents)
    {
        if (_wantedOrder.MatchDrink(contents))
        {
            print("wanted");
            return;
        }
        if (_neededOrder.MatchDrink(contents))
        {
            print("needed");
            return;
        }
        if (_dislikedOrder.MatchDrink(contents))
        {
            print("disliked");
            return;
        }
        print("none");
    }

    public void SetObjectActive(bool value)
    {
        gameObject.SetActive(value);
    }

}