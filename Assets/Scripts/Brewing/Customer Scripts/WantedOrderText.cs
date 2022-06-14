public class WantedOrderText : OrderText
{
    protected override void SetText()
    {
        _orderText.text = "I want " + Drink;
    }
}