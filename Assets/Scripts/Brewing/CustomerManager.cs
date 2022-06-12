using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Brewing;
using System;

// Displays customers
public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private List<Customer> _customerList;
    private Queue<CustomerData> _queue;

    // Start is called before the first frame update
    void Start()
    {
        _queue = CustomersData.GetQueue(1);
        foreach (Customer customer in _customerList)
        {
            customer.CustomerServed += OnCustomerServed;
            customer.SetObjectActive(false);
            DisplayNextCustomer(customer);

        }

    }

    private void OnDisable()
    {
        foreach (Customer customer in _customerList)
        {
            customer.CustomerServed -= OnCustomerServed;
        }
    }


    private void OnCustomerServed(Customer customer)
    {
        // _customerGameObject.SetActive(false);
        DisplayNextCustomer(customer);
    }

    private void DisplayNextCustomer(Customer customer)
    {
        try
        {
            DisplayCustomer(NextCustomer(), customer);
        }
        catch (InvalidOperationException)
        {
            print("No more customers.");
            customer.SetObjectActive(false);
        }
    }

    private void DisplayCustomer(CustomerData customerData, Customer customer)
    {
        customer.SetDrinks(customerData.Wanted, customerData.Needed, customerData.Disliked);
        customer.SetObjectActive(true);
    }

    private CustomerData NextCustomer()
    {
        return _queue.Dequeue();
    }


 
}
