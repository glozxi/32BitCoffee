public class NeededOrderText : OrderText
{
    protected override void SetText()
    {
        _orderText.text = "This guy needs " + Drink;
    }

    public void SetObjectInactive()
    {
        gameObject.SetActive(false);
    }
}
