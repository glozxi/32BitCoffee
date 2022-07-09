using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerWithBonusStub : ITimer
{
    private bool _hasBonus = true;
    public bool HasBonus
    { get => _hasBonus; }

    public void ResetTime()
    {
        _hasBonus = true;
    }

    public float GetRatioOfBonusTimeRemaining()
    {
        return 0.6f;
    }
}
