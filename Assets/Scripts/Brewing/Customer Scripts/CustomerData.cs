using Assets.Scripts.Brewing;

public class CustomerData
{
    public Drinks Wanted
    { get; set; }
    public Drinks Needed
    { get; set; }
    public Drinks Disliked
    { get; set; }

    public CustomerData(Drinks wanted, Drinks needed, Drinks disliked)
    {
        Wanted = wanted;
        Needed = needed;
        Disliked = disliked;
    }
}
