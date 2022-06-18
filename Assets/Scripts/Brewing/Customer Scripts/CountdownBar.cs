using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownBar : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private GameObject _textObject;

    private void Update()
    {
        _textObject.SetActive(!(_timer.GetRatioOfBonusTimeRemaining() <= 0));
        transform.localScale = new Vector2(_timer.GetRatioOfBonusTimeRemaining(), 1);
    }
}
