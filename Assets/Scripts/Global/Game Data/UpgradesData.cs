using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradesData : MonoBehaviour
{
    private static Dictionary<string, Upgrade> _data = new();
    public static Dictionary<string, Upgrade> Data
    { get => _data; }

    static UpgradesData()
    {
        foreach (Upgrade upgrade in Resources.LoadAll<Upgrade>("Upgrades"))
        {
            _data.Add(upgrade.Name, upgrade);
        }
    }

    public static List<string> GetStrFromUpgrades(List<Upgrade> upgrades)
    {
        return upgrades.Select(u => u.Name).ToList();
    }

    public static List<Upgrade> GetUpgradesFromStr(List<string> keys)
    {
        return keys.Select(k => _data[k]).ToList();
    }
}
