using UnityEngine;
using BrewingData;

// Ingredient box
public class Ingredient : DragOutItem
{
    [SerializeField]
    private IngredientScriptableObject _ing;
    public IngredientScriptableObject IngScriptable
    { get => _ing; }

    [SerializeField]
    private string _draggedOutTag = "Ingredient";
    protected override string DraggedOutTag
    { 
        get => _draggedOutTag; 
        set => _draggedOutTag = value; 
    }



}