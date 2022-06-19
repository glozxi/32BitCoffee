using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BrewingData;

public class DrinkPriceScript : MonoBehaviour
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

        _text.text = "$" + Recipes.GetPrice(drinkEnum).ToString();
    }

    private void OnDisable()
    {
        RecipeButton.DrinkButtonClicked -= SetName;
    }
}
