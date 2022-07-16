using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrewingData;

[CreateAssetMenu(fileName = "New Drink", menuName = "Drink")]
public class Drink : ScriptableObject
{
    [SerializeField]
    private Drinks _drinkType;
    public Drinks DrinkType
    { get => _drinkType; }

    [SerializeField]
    private float _price;
    public float Price
    { get => _price; }

    [SerializeField]
    private List<IngredientScriptableObject> _recipe;
    public List<IngredientScriptableObject> Recipe
    { get => _recipe; }
}
