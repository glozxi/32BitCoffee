using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Brewing;

// Stores data of customers of each round
public class CustomersData
{
    private static List<CustomerData> list1 = new() {
        new CustomerData(Drinks.Latte, Drinks.Latte, Drinks.Latte),
        new CustomerData(Drinks.Chocolate, Drinks.ChocoLatte, Drinks.ChocoLatte),
        new CustomerData(Drinks.Chocolate, Drinks.Latte, Drinks.ChocoLatte),
        new CustomerData(Drinks.Latte, Drinks.Latte, Drinks.Latte),
        new CustomerData(Drinks.Chocolate, Drinks.ChocoLatte, Drinks.ChocoLatte)
    };

    private static Queue<CustomerData> queue1 = new(list1);

    public static Queue<CustomerData> GetQueue(int number)
    {
        switch(number)
        {
            case 1:
                return queue1;
            default:
                Debug.LogError("Customer queue not found.");
                return null;
        }
    }


}
