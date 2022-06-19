using System;
using UnityEngine;
using UnityEngine.UI;
using BrewingData;

public class RecipeButtons : MonoBehaviour
{
    public delegate void ButtonMadeEventHandler(Drinks drink);
    public static event ButtonMadeEventHandler DrinkButtonMade;

    [SerializeField]
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Drinks drink in Enum.GetValues(typeof(Drinks)))
        {
            if (drink == Drinks.None) continue;
            Instantiate(_button, this.transform);
            DrinkButtonMade?.Invoke(drink);
        }
    }

}
