using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownBar : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private Slider _slider;

    private bool _isActive = true;

    private void Update()
    {
        // is active but shouldnt be, or not active but should be
        if (_isActive != _timer.IsWithinDuration)
        {
            SetActiveAllChildren(transform, _timer.IsWithinDuration);
        }
        _slider.value = _timer.GetRatioOfTimeRemaining();
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        _isActive = value;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }
}
