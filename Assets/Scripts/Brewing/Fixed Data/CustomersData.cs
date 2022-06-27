using System.Collections.Generic;
using UnityEngine;
using BrewingData;

// Stores data of customers of each round
public class CustomersData : MonoBehaviour
{
    private static Dictionary<string, CustomerData> _data = new();
    private static List<CustomerData> _listMS2;
    private static List<CustomerData> _list1;

    static CustomersData()
    {
        foreach (CustomerData customer in Resources.LoadAll<CustomerData>("Customer"))
        {
            _data.Add(customer.Name, customer);
        }
        _listMS2 = new()
        {
            _data["Arity"]
        };
        _list1 = new() 
        {
            _data["Xiao Mei"]
        };
    }

    public static Queue<CustomerData> GetQueue(string level)
    {
        switch(level)
        {
            case "tut_MS2":
                return new(_listMS2);
            case "tutorial":
                return new(_list1);
            default:
                Debug.LogError("Customer queue not found.");
                return null;
        }
    }

    public static string GetAnalyseInfo(string name)
    {
        return _data[name].AnalyseInformation;
    }

}
