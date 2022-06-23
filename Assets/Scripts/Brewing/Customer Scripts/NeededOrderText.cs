public class NeededOrderText : OrderText
{
    protected override void SetText()
    {
        _orderText.text = _textToDisplay;
    }

    public void SetObjectInactive()
    {
        gameObject.SetActive(false);
    }
}
