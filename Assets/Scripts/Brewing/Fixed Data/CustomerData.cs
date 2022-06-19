using UnityEngine;
using BrewingData;

public class CustomerData
{
    public Drinks Wanted
    { get; set; }
    public Drinks Needed
    { get; set; }
    public Drinks Disliked
    { get; set; }

    private Sprite _sprite;
    // Change from string to enum?
    private string _mood;
    private string _promptDialogue;
    // name?
    private string _analyzeInformation;
    private float _bonusTime;
    private float _bonusAmount;

    public CustomerData(Drinks wanted, Drinks needed, Drinks disliked)
    {
        Wanted = wanted;
        Needed = needed;
        Disliked = disliked;
    }
}
