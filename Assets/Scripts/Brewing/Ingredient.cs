using UnityEngine;
public class Ingredient : DragOutItem
{
    [SerializeField]
    private string ingredientType;
    public string IngredientType
    {
        get => ingredientType;
    }

    [SerializeField]
    private string ingredientName;
    public string IngredientName
    {
        get => ingredientName;
    }
}
