using UnityEngine;
using TMPro;
using Assets.Scripts.Brewing;

public abstract class OrderText : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text _orderText;

    protected Drinks _drink;
    public Drinks Drink
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
