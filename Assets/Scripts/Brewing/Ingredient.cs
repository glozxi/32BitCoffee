using UnityEngine;

public class Ingredient : DragOutItem
{
    [SerializeField]
    private Recipes.Ingredients _ingredientType;
    public Recipes.Ingredients IngredientType
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