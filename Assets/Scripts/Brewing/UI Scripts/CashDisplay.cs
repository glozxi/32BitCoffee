public class CashDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        Points.CashUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.CashUpdated -= OnAmountUpdate;
    }

}
