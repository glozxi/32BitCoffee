using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;
using UnityEditor;

public class Recipes : MonoBehaviour
{
    [SerializeField]
    private List<Drink> _drinksList;

    private static Dictionary<Drinks, Drink> recipes = new();

    static Recipes()
    {
        foreach (Drink drink in Resources.LoadAll<Drink>("Drink"))
        {
            recipes.Add(drink.DrinkType, drink);
        }

    }

    public static Drink GetDrink(Drinks drink)
    {
        return recipes[drink];
    }

    public static List<Ingredients> GetRecipe(Drinks drinkName)
    {
        return recipes[drinkName].Recipe;
    }

    public static float GetPrice(Drinks drinkName)
    {
        return recipes[drinkName].Price;
    }


}
