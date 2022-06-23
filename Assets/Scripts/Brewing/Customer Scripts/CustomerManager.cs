using System;
using System.Collections.Generic;
using UnityEngine;

// Displays customers
public class CustomerManager : MonoBehaviour
{
    public delegate void ServeEndEventHandler();
    public static event ServeEndEventHandler AllServed;

    [SerializeField]
    private List<Customer> _customerList;
    private Queue<CustomerData> _queue;

    // Start is called before the first frame update
    void Start()
    {
        _queue = CustomersData.GetQueue(State.NextBrewLevel);
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
            customer.SetObjectActive(false);
            if (IsServeFinished())
            {
                AllServed?.Invoke();
            }
        }
    }

    private void DisplayCustomer(CustomerData customerData, Customer customer)
    {
        customer.SetNewCustomer(customerData.Wanted, customerData.Needed, customerData.Disliked);
        customer.SetObjectActive(true);
    }

    private CustomerData NextCustomer()
    {
        return _queue.Dequeue();
    }

    private bool IsServeFinished()
    {
        return _customerList.TrueForAll(customer => !customer.IsActive());
    }
 
}
