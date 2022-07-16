using System.Collections.Generic;
using UnityEngine;
using BrewingData;
using UnityEditor;

public class Recipes : MonoBehaviour
{
    private static Dictionary<Drinks, Drink> _recipes = new();
    private static List<IngredientScriptableObject> _ingredientList = new();

    static Recipes()
    {
        foreach (Drink drink in Resources.LoadAll<Drink>("Drink"))
        {
            _recipes.Add(drink.DrinkType, drink);
        }
        foreach (IngredientScriptableObject ing in Resources.LoadAll<IngredientScriptableObject>("Ingredient"))
        {
            _ingredientList.Add(ing);
        }

    }

    public static List<IngredientScriptableObject> GetRecipe(Drinks drinkName)
    {
        return _recipes[drinkName].Recipe;
    }

    public static float GetPrice(Drinks drinkName)
    {
        return _recipes[drinkName].Price;
    }


}
