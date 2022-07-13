using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name
    { get => _name; }

    // Next upgrade stacks
    [SerializeField]
    private Upgrade _nextUpgrade;
    public Upgrade NextUpgrade
    { get => _nextUpgrade; }

    [SerializeField]
    private Upgrade _prevUpgrade;
    public Upgrade PrevUpgrade
    { get => _prevUpgrade; }


    [SerializeField]
    [TextArea(15, 20)]
    private string _description;
    public string Description
    { get => _description; }

    [SerializeField]
    private float _cost;
    public float Cost
    { get => _cost; }

    public bool IsBaseUpgrade()
    {
        return _prevUpgrade == null;
    }
}
