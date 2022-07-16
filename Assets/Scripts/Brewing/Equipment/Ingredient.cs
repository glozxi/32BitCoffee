using UnityEngine;
using BrewingData;

public class Ingredient : DragOutItem
{
    [SerializeField]
    private IngredientScriptableObject _ing;
    public IngredientScriptableObject IngScriptable
    { get => _ing; }

    private const string INGREDIENT_TAG = "Ingredient";
    protected override string DraggedOutTag
    { get; set; } = INGREDIENT_TAG;



}