public class WantedOrderText : OrderText
{
    protected override void SetText()
    {
        _orderText.text = _textToDisplay;
    }
}