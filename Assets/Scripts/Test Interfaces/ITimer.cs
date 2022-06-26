using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    bool HasBonus 
    { get; }

    public void ResetTime();

    public float GetRatioOfBonusTimeRemaining();
}
