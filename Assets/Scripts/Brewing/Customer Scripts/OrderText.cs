using UnityEngine;
using TMPro;
using BrewingData;

public abstract class OrderText : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text _orderText;

    protected string _textToDisplay;
    public string TextToDisplay
    {
        get => _textToDisplay; 
        set
        {
            _textToDisplay = value;
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
