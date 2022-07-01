using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Current amount of cash
public class LevelCash : MonoBehaviour
{
    private const float BONUS_MULTIPLIER = 2;

    private float _cashGoal;
    private float _currentCash = 0;
    private State _state;
    [SerializeField]
    private CashCurrentUI _ui;

    // Start is called before the first frame update
    void Start()
    {
        _state = FindObjectOfType<State>();
        _cashGoal = LevelsData.GetCashGoal(_state.NextBrewLevel);
        _ui.Goal = _cashGoal;
    }

    public void AddCash(IOrder order, ITimer timer)
    {
        _currentCash += timer.HasBonus ? order.GetPrice() * BONUS_MULTIPLIER : order.GetPrice();
        _ui.CurrentCash = _currentCash;
    }
}
