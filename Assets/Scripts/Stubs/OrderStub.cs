using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrewingData;

public class OrderStub : IOrder
{
    private bool _isMatch;
    private float _price;

    public OrderStub(bool isMatch, float price)
    {
        _isMatch = isMatch;
        _price = price;
    }

    public bool DoesDrinkMatch(List<Ingredients> actualIngredients)
    {
        return _isMatch;
    }

    public float GetPrice()
    {
        return _price;
    }
}
