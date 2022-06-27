using UnityEngine;
using BrewingData;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class CustomerData : ScriptableObject
{

    [SerializeField]
    private string _name;
    public string Name
    { get => _name; }

    [SerializeField]
    private Drinks _wanted;
    public Drinks Wanted
    { get => _wanted; }

    [SerializeField]
    private Drinks _needed;
    public Drinks Needed
    { get => _needed; }

    [SerializeField]
    private Drinks _disliked;
    public Drinks Disliked
    { get => _disliked; }

    [SerializeField]
    private Sprite _mainSprite;
    public Sprite MainSprite
    { get => _mainSprite; }

    [SerializeField]
    private Sprite _expression;
    public Sprite Expression
    { get => _expression; }

    // Change from string to enum?
    [SerializeField]
    private string _mood = "normal";

    [SerializeField]
    private string _promptDialogue = "Where's my drink?";

    [SerializeField]
    [TextArea(15, 20)]
    private string _orderTalk;
    public string OrderTalk
    { get => _orderTalk; }

    [SerializeField]
    [TextArea(15, 20)]
    private string _analyseInformation;
    public string AnalyseInformation
    { get => _analyseInformation; }

    [SerializeField]
    private float _bonusTime = 10f;

    [SerializeField]
    private float _bonusMult = 1.5f;

    // True if order served affects the story
    [SerializeField]
    private bool _isStoryAffected;
    public bool IsStoryAffected
    { get => _isStoryAffected; }


}
