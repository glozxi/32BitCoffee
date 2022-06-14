using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;

public class Recipes : MonoBehaviour
{
    public static Dictionary<Drinks, List<Ingredients>> recipes = new()
    {
        { 
            Drinks.Latte, new()
            {
                Ingredients.Espresso,
                Ingredients.Espresso,
                Ingredients.Espresso,
                Ingredients.Espresso
            }
        },
        {
            Drinks.Chocolate, new()
            {
                Ingredients.Chocolate,
                Ingredients.Chocolate,
                Ingredients.Chocolate,
                Ingredients.Chocolate
            }
        },
        {
            Drinks.ChocoLatte, new()
            {
                Ingredients.Chocolate,
                Ingredients.Chocolate,
                Ingredients.Espresso,
                Ingredients.Espresso
            }
        }
    };

    public static Dictionary<Drinks, float> prices = new()
    {
        {
            Drinks.Latte, 4f
        },
        {
            Drinks.Chocolate, 2f
        },
        {
            Drinks.ChocoLatte, 3f
        }
    };


}
