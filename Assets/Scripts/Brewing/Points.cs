using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Points : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pointsText;
    private int points = 0;

    public void UpdatePoints(int toAdd)
    {
        points += toAdd;
        pointsText.text = points.ToString();
    }
}
