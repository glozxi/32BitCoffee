using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    bool IsWithinDuration 
    { get; }

    public void ResetTime();

    public float GetRatioOfTimeRemaining();
}
