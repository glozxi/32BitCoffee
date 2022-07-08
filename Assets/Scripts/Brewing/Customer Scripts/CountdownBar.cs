using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownBar : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private GameObject _textObject;

    [SerializeField]
    private Slider _slider;

    private bool _isActive = true;

    private void Update()
    {
        // is active but shouldnt be, or not active but should be
        if (_isActive == _timer.GetRatioOfBonusTimeRemaining() <= 0)
        {
            SetActiveAllChildren(transform, !(_timer.GetRatioOfBonusTimeRemaining() <= 0));
            _isActive = !(_timer.GetRatioOfBonusTimeRemaining() <= 0);
        }
        _slider.value = _timer.GetRatioOfBonusTimeRemaining();
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }
}
