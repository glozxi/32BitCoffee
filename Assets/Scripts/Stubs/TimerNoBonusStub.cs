using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNoBonusStub : ITimer
{
    private bool _hasBonus = false;
    public bool IsWithinDuration
    { get => _hasBonus; }

    public void ResetTime()
    {
        _hasBonus = true;
    }

    public float GetRatioOfTimeRemaining()
    {
        return 0.6f;
    }
}
