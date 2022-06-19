public class Points
{
    public delegate void PointsUpdateEventHandler(float amount);
    public static event PointsUpdateEventHandler CashUpdated;
    public static event PointsUpdateEventHandler NetworkPointsUpdated;

    private static float _cash = 0f;
    private static float _networkPoints = 0f;
    private static readonly float BonusMultiplier = 2;
    private static readonly float PointsToAdd = 5;


    public static void AddCash(Order order, Timer timer)
    {
        _cash += timer.HasBonus ? order.GetPrice() * BonusMultiplier : order.GetPrice();
        CashUpdated?.Invoke(_cash);
    }

    public static void AddAnalysePoints()
    {
        _networkPoints += PointsToAdd;
        NetworkPointsUpdated?.Invoke(_networkPoints);
    }
}
