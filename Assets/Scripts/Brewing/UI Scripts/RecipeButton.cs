using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Brewing;

public class RecipeButton : MonoBehaviour
{
    public delegate void ButtonClickedEventHandler(Drinks drinkEnum);
    public static event ButtonClickedEventHandler DrinkButtonClicked;

    private Button _button;

    void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        RecipeButtons.DrinkButtonMade += SetButton;
    }

    private void SetButton(Drinks drinkEnum)
    {
        TMP_Text buttonText = _button.GetComponentInChildren<TMP_Text>();
        buttonText.text = drinkEnum.ToString();

        _button.onClick.AddListener(() => TaskOnClick(drinkEnum));
        RecipeButtons.DrinkButtonMade -= SetButton;
    }

    private void TaskOnClick(Drinks drinkEnum)
    {
        DrinkButtonClicked?.Invoke(drinkEnum);
    }
}
