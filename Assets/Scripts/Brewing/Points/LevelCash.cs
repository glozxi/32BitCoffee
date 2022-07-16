using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Current amount of cash
public class LevelCash : MonoBehaviour
{
    private float _cashGoal;
    public float CurrentCash
    { get; set; } = 0;
    [SerializeField]
    private CashCurrentUI _ui;

    // Start is called before the first frame update
    void Start()
    {
        _cashGoal = LevelsData.GetCashGoal(State.Instance.NextBrewLevel);
        _ui.Goal = _cashGoal;
    }

    // Adds cash to current cash
    public void AddCash(Order order, ITimer timer, float bonusMult)
    {
        CurrentCash += timer.HasBonus ? order.GetPrice() * bonusMult : order.GetPrice();
        _ui.CurrentCash = CurrentCash;
    }

    public bool IsGoalReached()
    {
        return CurrentCash >= _cashGoal;
    }
}