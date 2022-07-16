using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Time Upgrade")]
public class TimeUpgrade : Upgrade
{
    [SerializeField]
    private float _addedTime;
    public float AddedTime
    { get => _addedTime; }

    [SerializeField]
    private float _timeScale = 1;
    public float TimeScale
    { get => _timeScale; }

    private void Reset()
    {
        _addedTime = 0;
        _timeScale = 1;
    }

    public void UseUpgrade(Timer timer)
    {
        timer.Duration += _addedTime;
        timer.TimeRate *= _timeScale;
    }
}
