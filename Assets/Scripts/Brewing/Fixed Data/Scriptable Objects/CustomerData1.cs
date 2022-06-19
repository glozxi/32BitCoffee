using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrewingData;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class CustomerData1 : ScriptableObject
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
    private Sprite _sprite;

    // Change from string to enum?
    [SerializeField]
    private string _mood = "normal";

    [SerializeField]
    private string _promptDialogue = "Where's my drink?";

    // name?
    [SerializeField]
    private string _analyzeInformation;

    [SerializeField]
    private float _bonusTime = 10f;

    [SerializeField]
    private float _bonusMult = 1.5f;


}
