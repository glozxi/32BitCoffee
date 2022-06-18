using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timer = 0.0f;

    [SerializeField]
    private float BonusDuration = 7.0f;
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
