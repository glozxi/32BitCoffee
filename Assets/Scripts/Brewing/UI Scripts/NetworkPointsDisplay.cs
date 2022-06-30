public class NetworkPointsDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        Points points = FindObjectOfType<Points>();
        _text.text = points.NetworkPoints.ToString();
        Points.NetworkPointsUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.NetworkPointsUpdated -= OnAmountUpdate;
    }
}
