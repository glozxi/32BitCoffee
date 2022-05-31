using UnityEngine;

public class Ingredient : DragOutItem
{
    [SerializeField]
    private Recipes.Ingredients _ingredientType;
    public Recipes.Ingredients IngredientType
    {
        get => _ingredientType;
    }

}