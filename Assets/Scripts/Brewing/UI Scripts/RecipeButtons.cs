using System;
using UnityEngine;
using UnityEngine.UI;
using BrewingData;
using System.Linq;

public class RecipeButtons : MonoBehaviour
{
    public delegate void ButtonMadeEventHandler(Drinks drink);
    public static event ButtonMadeEventHandler DrinkButtonMade;

    [SerializeField]
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        Drinks[] allDrinks = Enum.GetValues(typeof(Drinks)).Cast<Drinks>().ToArray();
        var sortedDrinks = allDrinks.OrderBy(l => l.ToString());

        foreach (Drinks drink in sortedDrinks)
        {
            if (drink == Drinks.None) continue;
            Instantiate(_button, this.transform);
            DrinkButtonMade?.Invoke(drink);
        }
    }

}
