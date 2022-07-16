using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void TimeEventHandler();
    public event TimeEventHandler TimeEnd;

    private float _timer = 0.0f;

    public float Duration
    { get; set; } = 7.0f;

    public float TimeRate
    { get; set; } = 1f;

    // Has started and not ended
    private bool _isWithinDuration = false;
    public bool IsWithinDuration
    { get => _isWithinDuration; }

    // Update is called once per frame
    void Update()
    {
        if (_isWithinDuration)
        {
            _timer += Time.deltaTime * TimeRate;
            if (_timer > Duration)
            {
                _isWithinDuration = false;
                TimeEnd?.Invoke();
            }
        }
    }

    public void StartTime()
    {
        _isWithinDuration = true;
    }

    // Set time back to 0
    public void ResetTime()
    {
        _timer = 0.0f;
        _isWithinDuration = false;
    }

    // Remaining/Total
    public float GetRatioOfTimeRemaining()
    {
        if (_isWithinDuration)
        {
            return 1 - _timer / Duration;
        }
        return 0f;
    }
}
