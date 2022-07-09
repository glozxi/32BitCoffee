using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    private float _timer = 0.0f;

    public float BonusDuration
    { get; set; } = 7.0f;

    public float TimeRate
    { get; set; } = 1f;

    private bool _hasBonus = true;
    public bool HasBonus
    { get => _hasBonus; }

    // Update is called once per frame
    void Update()
    {
        if (_hasBonus)
        {
            _timer += Time.deltaTime * TimeRate;
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

    // Remaining/Total
    public float GetRatioOfBonusTimeRemaining()
    {
        if (_hasBonus)
        {
            return 1 - _timer / BonusDuration;
        }
        return 0f;
    }
}
