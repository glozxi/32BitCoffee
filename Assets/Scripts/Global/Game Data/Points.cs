using UnityEngine;

public class Points : MonoBehaviour
{
    public delegate void PointsUpdateEventHandler(float amount);
    public static event PointsUpdateEventHandler CashUpdated;
    public static event PointsUpdateEventHandler NetworkPointsUpdated;

    private const float BONUS_MULTIPLIER = 2;
    private const float POINTS_TO_ADD = 5;

    private float _cash = 0f;
    public float Cash
    {
        get => _cash;
    }

    private float _networkPoints = 0f;
    public float NetworkPoints
    {
        get => _networkPoints;
    }


    public void AddCash(IOrder order, ITimer timer)
    {
        _cash += timer.HasBonus ? order.GetPrice() * BONUS_MULTIPLIER : order.GetPrice();
        CashUpdated?.Invoke(_cash);
    }

    public void AddAnalysePoints()
    {
        _networkPoints += POINTS_TO_ADD;
        NetworkPointsUpdated?.Invoke(_networkPoints);
    }

    public void LoadPoints(float cash, float networkPoints)
    {
        _cash = cash;
        _networkPoints = networkPoints;
    }
}
