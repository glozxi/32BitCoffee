public class Points
{
    public delegate void PointsUpdateEventHandler(float amount);
    public static event PointsUpdateEventHandler CashUpdated;
    public static event PointsUpdateEventHandler NetworkPointsUpdated;

    private static readonly float BONUS_MULTIPLIER = 2;
    private static readonly float POINTS_TO_ADD = 5;

    private static float _cash = 0f;
    public static float Cash
    {
        get => _cash;
    }

    private static float _networkPoints = 0f;
    public static float NetworkPoints
    {
        get => _networkPoints;
    }


    public static void AddCash(IOrder order, ITimer timer)
    {
        _cash += timer.HasBonus ? order.GetPrice() * BONUS_MULTIPLIER : order.GetPrice();
        CashUpdated?.Invoke(_cash);
    }

    public static void AddAnalysePoints()
    {
        _networkPoints += POINTS_TO_ADD;
        NetworkPointsUpdated?.Invoke(_networkPoints);
    }

    public static void LoadPoints(float cash, float networkPoints)
    {
        _cash = cash;
        _networkPoints = networkPoints;
    }
}
