using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Brewing;

public class DrinkDetailsScript : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        RecipeButton.DrinkButtonClicked += SetName;

    }

    private void SetName(Drinks drinkEnum)
    {

        string descriptionText = "";
        foreach (Ingredients ingredient in Recipes.GetRecipe(drinkEnum))
        {
            descriptionText += ingredient.ToString() + "\n";
        }
        _text.text = descriptionText;
    }

    private void OnDisable()
    {
        RecipeButton.DrinkButtonClicked -= SetName;
    }
}
