using UnityEngine;
using TMPro;

public abstract class OrderText : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text _orderText;

    protected Recipes.Drinks _drink;
    public Recipes.Drinks Drink
    {
        get => _drink; 
        set
        {
            _drink = value;
            SetText();

        }
    }

    private void Awake()
    {
        _orderText = GetComponent<TMP_Text>();
        SetText();
    }

    protected abstract void SetText();
}
