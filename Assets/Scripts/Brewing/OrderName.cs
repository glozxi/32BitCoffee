using UnityEngine;
using TMPro;

public class OrderName : MonoBehaviour
{
    private TMP_Text _orderText;
    public Recipes.Drinks Wanted
    { get; set; }
    public Recipes.Drinks Needed
    { get; set; }
    private bool _isWantedDisplayed;

    void Awake()
    {
        _orderText = gameObject.GetComponent<TMP_Text>();
        SetWanted();
    }

    public void ToggleOrder()
    {
        if (_isWantedDisplayed)
        {
            SetNeeded();
        }
        else
        {
            SetWanted();
        }
    }

    private void SetWanted()
    {
        _orderText.text = "I want " + Wanted.ToString();
        _isWantedDisplayed = true;
    }

    private void SetNeeded()
    {
        _orderText.text = "This guy needs " + Needed.ToString();
        _isWantedDisplayed = false;
    }
}
