using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    private float _timer = 0.0f;

    [SerializeField]
    private float _bonusDuration = 7.0f;
    private bool _hasBonus = true;
    public bool HasBonus
    { get => _hasBonus; }

    // Update is called once per frame
    void Update()
    {
        if (_hasBonus)
        {
            _timer += Time.deltaTime;
            if (_timer > _bonusDuration)
            {
                print(_bonusDuration + " seconds");
                _hasBonus = false;
            }
        }
    }

    public void ResetTime()
    {
        _timer = 0.0f;
        _hasBonus = true;
    }

    // Remaining/Total
    public float GetRatioOfBonusTimeRemaining()
    {
        if (_hasBonus)
        {
            return 1 - _timer / _bonusDuration;
        }
        return 0f;
    }
}
