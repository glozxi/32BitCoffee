using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timer = 0.0f;
    private const float BonusDuration = 7.0f;
    private bool _hasBonus = true;
    public bool HasBonus
    { get => _hasBonus; }

    // Update is called once per frame
    void Update()
    {
        if (_hasBonus)
        {
            _timer += Time.deltaTime;
            if (_timer > BonusDuration)
            {
                print(BonusDuration + " seconds");
                _hasBonus = false;
            }
        }
        
    }

    public void ResetTime()
    {
        _timer = 0.0f;
        _hasBonus = true;
    }
}
