using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string InkStoryState
    { get; set; }

    public string TextLog
    { get; set; }
    public string Day
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
    public DateTime Time
    { get; set; }
    public string BGMFile
    { get; set; }
    public List<CharData> CharDatas
    { get; set; }
    public List<string> ActiveUpgrades
    { get; set; }
}