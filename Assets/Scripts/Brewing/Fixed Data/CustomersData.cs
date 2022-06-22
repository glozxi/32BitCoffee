using System.Collections.Generic;
using UnityEngine;
using BrewingData;

// Stores data of customers of each round
public class CustomersData
{
    private static List<CustomerData> list1 = new() {
        new CustomerData(Drinks.Latte, Drinks.Latte, Drinks.Latte)
    };

    public static Queue<CustomerData> GetQueue(int number)
    {
        switch(number)
        {
            case 1:
                return new(list1);
            default:
                Debug.LogError("Customer queue not found.");
                return null;
        }
    }


}
