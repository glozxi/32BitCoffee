public class CashDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        Points points = FindObjectOfType<Points>();
        _text.text = points.Cash.ToString();
        Points.CashUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.CashUpdated -= OnAmountUpdate;
    }

}
