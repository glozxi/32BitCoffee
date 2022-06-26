using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNoBonusStub : ITimer
{
    private float _timer = 5.0f;

    private bool _hasBonus = false;
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
