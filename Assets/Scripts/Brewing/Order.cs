using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Brewing;

public class Order
{
    private Drinks _drink;

    private OrderTypes _type;

    public Order(Drinks drink, OrderTypes type)
    {
        _drink = drink;
        _type = type;
    }

    public bool MatchDrink(List<Ingredients> actualIngredients)
    {
        return Recipes.recipes[_drink].SequenceEqual(actualIngredients);
    }

    public float GetPrice()
    {
        if (_type == OrderTypes.Needed || _type == OrderTypes.Wanted)
        {
            return Recipes.prices[_drink];
        }
        return 0f;
    }

}
