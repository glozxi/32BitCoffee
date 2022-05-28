using UnityEngine;

public class Ingredient : DragOutItem
{
    [SerializeField]
    private string _ingredientType;
    public string IngredientType
    {
        get => _ingredientType;
    }

    [SerializeField]
    private string _ingredientName;
    public string IngredientName
    {
        get => _ingredientName;
    }
}
