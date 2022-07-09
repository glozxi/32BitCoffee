using System;
using System.Collections.Generic;
using UnityEngine;

// Stores data of customers of each round
public class LevelsData : MonoBehaviour
{
    private static Dictionary<string, Level> _data = new();

    static LevelsData()
    {
        foreach (Level level in Resources.LoadAll<Level>("Levels"))
        {
            _data.Add(level.LevelName, level);
        }
    }

    public static Queue<CustomerData> GetQueue(string level)
    {
        try
        {
            return new(_data[level].CustomerDatas);
        }
        catch (KeyNotFoundException)
        {
            Debug.LogWarning("Level not found. Returning default queue.");
            return new(_data["default"].CustomerDatas);
        }
        catch (ArgumentNullException)
        {
            Debug.LogWarning("Level name is null. Returning default queue. ");
            return new(_data["default"].CustomerDatas);
        }
    }

    public static float GetCashGoal(string level)
    {
        try
        {
            return _data[level].CashGoal;
        }
        catch (KeyNotFoundException)
        {
            Debug.LogWarning("Level not found. Returning default.");
            return _data["default"].CashGoal;
        }
        catch (ArgumentNullException)
        {
            Debug.LogWarning("Level name is null. Returning default. ");
            return _data["default"].CashGoal;
        }
    }

}
