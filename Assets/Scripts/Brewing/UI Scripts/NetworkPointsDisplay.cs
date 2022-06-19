public class NetworkPointsDisplay : UIPointsDisplay
{
    protected override void OnEnable()
    {
        Points.NetworkPointsUpdated += OnAmountUpdate;
    }

    protected override void OnDisable()
    {
        Points.NetworkPointsUpdated -= OnAmountUpdate;
    }
}
