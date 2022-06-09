using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Recipes.Drinks _wantedDrink;
    private Order _wantedOrder;

    [SerializeField]
    private Recipes.Drinks _neededDrink;
    private Order _neededOrder;

    [SerializeField]
    private Recipes.Drinks _dislikedDrink;
    private Order _dislikedOrder;

    [SerializeField]
    private WantedOrderText _wantedText;
    [SerializeField]
    private NeededOrderText _neededText;

    [SerializeField]
    private Servebox _servebox;

    private void Awake()
    {
        _wantedOrder = new Order(_wantedDrink);
        _neededOrder = new Order(_neededDrink);
        _dislikedOrder = new Order(_dislikedDrink);

    }

    private void OnEnable()
    {
        _servebox.CupCollision += OnServed;

        _wantedText.Drink = _wantedDrink;
        _neededText.Drink = _neededDrink;

    }

    private void OnDisable()
    {
        _servebox.CupCollision -= OnServed;
    }

    private void OnServed(Cup cup)
    {
        CheckDrink(cup.Contents);
    }

    private void CheckDrink(List<Recipes.Ingredients> contents)
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

}