using UnityEngine;
using BrewingData;

public class Ingredient : DragOutItem
{
    private const string INGREDIENT_TAG = "Ingredient";
    protected override string DraggedOutTag
    { get; set; } = INGREDIENT_TAG;

    [SerializeField]
    private Ingredients _ingredientType;
    public Ingredients IngredientType
    {
        get => _ingredientType;
    }

    [SerializeField]
    private Color32 _contentColor;
    public Color32 ContentColor
    {
        get => _contentColor;
    }


}