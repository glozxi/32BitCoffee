using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;

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
    private List<Ingredients> _recipe;
    public List<Ingredients> Recipe
    { get => _recipe; }
}
