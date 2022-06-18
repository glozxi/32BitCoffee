public class Cash
{
    public delegate void CashUpdateEventHandler(float cash);
    public static event CashUpdateEventHandler CashUpdated;

    private static float _cash = 0f;
    private static readonly float BonusMultiplier = 2;


    public static void AddCash(Order order, Timer timer)
    {
        _cash += timer.HasBonus ? order.GetPrice() * BonusMultiplier : order.GetPrice();
        CashUpdated?.Invoke(_cash);
    }
}
