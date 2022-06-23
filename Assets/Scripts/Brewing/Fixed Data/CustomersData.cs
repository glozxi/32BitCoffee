using System.Collections.Generic;
using UnityEngine;
using BrewingData;

// Stores data of customers of each round
public class CustomersData : MonoBehaviour
{
    private static Dictionary<string, CustomerData> _data = new();
    private static List<CustomerData> list1;

    static CustomersData()
    {
        foreach (CustomerData customer in Resources.LoadAll<CustomerData>("Customer"))
        {
            _data.Add(customer.Name, customer);
        }
        list1 = new() {
            _data["Xiao Mei"]
        };
    }

    public static Queue<CustomerData> GetQueue(string level)
    {
        switch(level)
        {
            case "tutorial":
                return new(list1);
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
