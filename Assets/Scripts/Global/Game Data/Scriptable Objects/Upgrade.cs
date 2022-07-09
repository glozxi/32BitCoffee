using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name
    { get => _name; }

    [SerializeField]
    [TextArea(15, 20)]
    private string _description;
    public string Description
    { get => _description; }

    [SerializeField]
    private float _cost;
    public float Cost
    { get => _cost; }
}
