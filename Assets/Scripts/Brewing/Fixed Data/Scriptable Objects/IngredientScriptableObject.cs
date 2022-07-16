using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class IngredientScriptableObject : ScriptableObject
{
    [SerializeField]
    private string _ingredientType;
    public string IngredientType
    { get => _ingredientType; }

    [SerializeField]
    [TextArea(15, 20)]
    private string _step;
    public string Step
    { get => _step; }


    [SerializeField]
    private Color32 _contentColor;
    public Color32 ContentColor
    {
        get => _contentColor;
    }
}
