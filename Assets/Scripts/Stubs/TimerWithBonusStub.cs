using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerWithBonusStub : ITimer
{
    private float _timer = 0.0f;

    private bool _hasBonus = true;
    public bool HasBonus
    { get => _hasBonus; }

    public void ResetTime()
    {
        _hasBonus = true;
        _timer = 0.0f;
    }

    public float GetRatioOfBonusTimeRemaining()
    {
        return 0.6f;
    }
}
