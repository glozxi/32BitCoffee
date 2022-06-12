using UnityEngine;
using Assets.Scripts.Brewing;

public class Ingredient : DragOutItem
{
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