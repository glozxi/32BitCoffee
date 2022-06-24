public class CashDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        _text.text = Points.Cash.ToString();
        Points.CashUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.CashUpdated -= OnAmountUpdate;
    }

}
