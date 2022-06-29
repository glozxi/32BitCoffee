using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string InkStoryState
    { get; set; }

    public string TextLog
    { get; set; }

    public float Cash
    { get; set; }
    public float NetworkPoints
    { get; set; }
    public string NextBrewLevel
    { get; set; }
    public int Outcome
    { get; set; }
    public string Drink
    { get; set; }
}
