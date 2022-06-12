using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;

public class Recipes : MonoBehaviour
{
    public static Dictionary<Drinks, List<Ingredients>> recipes = new Dictionary<Drinks, List<Ingredients>>()
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


}
