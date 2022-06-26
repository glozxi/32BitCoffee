using System.Collections;
using System.Collections.Generic;
using BrewingData;

public interface IOrder
{
    public bool DoesDrinkMatch(List<Ingredients> actualIngredients);

    public float GetPrice();
}
