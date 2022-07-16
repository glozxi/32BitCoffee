using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BrewingData;

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
        int stepNum = 1;
        string descriptionText = "";
        foreach (IngredientScriptableObject ingredient in Recipes.GetRecipe(drinkEnum))
        {
            descriptionText += stepNum + ". " + ingredient.Step + "\n";
            stepNum++;
        }
        _text.text = descriptionText;
    }

    private void OnDisable()
    {
        RecipeButton.DrinkButtonClicked -= SetName;
    }
}
