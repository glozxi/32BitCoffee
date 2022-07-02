using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashCurrentUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private float _goal = 0;
    public float Goal
    {
        get => _goal;
        set
        {
            _goal = value;
            ChangeDisplayText();
        }
    }

    private float _currentCash = 0;
    public float CurrentCash
    { 
        get => _currentCash; 
        set
        {
            _currentCash = value;
            ChangeDisplayText();
        }
    }

    private void ChangeDisplayText()
    {
        _text.text = "+" + CurrentCash + "/" + Goal;
    }

}
