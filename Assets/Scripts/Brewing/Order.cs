using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Order : MonoBehaviour
{
    [SerializeField]
    private Recipes.Drinks _drink;

    [SerializeField]
    private TMP_Text _orderText;

    [SerializeField]
    private Points _points;

    private void Start()
    {
        string text = _drink.ToString() + "\n";
        foreach (var ingredient in Recipes.recipes[_drink])
        {
            text += ingredient.ToString() + "\n";
        }
        _orderText.text = text;
    }

    public void MatchDrink(List<Recipes.Ingredients> actualIngredients)
    {
        if (Recipes.recipes[_drink].SequenceEqual(actualIngredients))
        {
            _points.UpdatePoints();
        }
    }

}
