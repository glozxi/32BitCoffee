using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Brewing;

public class DrinkNameScript : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        RecipeButton.DrinkButtonClicked += SetName;

    }

    private void SetName(Drinks drinkEnum)
    {
        _text.text = drinkEnum.ToString();
    }

    private void OnDisable()
    {
        RecipeButton.DrinkButtonClicked -= SetName;
    }
}
