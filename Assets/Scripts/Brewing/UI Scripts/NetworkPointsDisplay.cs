public class NetworkPointsDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        _text.text = Points.NetworkPoints.ToString();
        Points.NetworkPointsUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.NetworkPointsUpdated -= OnAmountUpdate;
    }
}
