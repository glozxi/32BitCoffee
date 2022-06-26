using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrewingData;

public class OrderStub : IOrder
{
    public bool DoesDrinkMatch(List<Ingredients> actualIngredients)
    {
        return true;
    }

    public float GetPrice()
    {
        return 7.5f;
    }
}
