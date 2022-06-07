using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Order
{
    [SerializeField]
    private Recipes.Drinks _drink;

    public Order(Recipes.Drinks drink)
    {
        _drink = drink;
    }

    public bool MatchDrink(List<Recipes.Ingredients> actualIngredients)
    {
        return Recipes.recipes[_drink].SequenceEqual(actualIngredients);
    }

}
