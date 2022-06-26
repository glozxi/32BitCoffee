using System.Collections.Generic;
using UnityEngine;
using BrewingData;
using UnityEditor;

public class Recipes : MonoBehaviour
{
    private static Dictionary<Drinks, Drink> _recipes = new();

    static Recipes()
    {
        foreach (Drink drink in Resources.LoadAll<Drink>("Drink"))
        {
            _recipes.Add(drink.DrinkType, drink);
        }

    }

    public static List<Ingredients> GetRecipe(Drinks drinkName)
    {
        return _recipes[drinkName].Recipe;
    }

    public static float GetPrice(Drinks drinkName)
    {
        return _recipes[drinkName].Price;
    }


}
