using System;
using System.Collections.Generic;
using UnityEngine;
using BrewingData;

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
        _queue = LevelsData.GetQueue(State.Instance.NextBrewLevel);
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


    private void OnCustomerServed(Customer customer, int outcome, Drinks drink)
    {
        // Set outcome, drink of current customer
        if (customer.IsStoryAffected)
        {
            State.Instance.Outcome = outcome;
            State.Instance.Drink = drink.ToString();
        }
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
        customer.SetNewCustomer(customerData);
        customer.SetObjectActive(true);
        AudioManager.instance.PlaySFX("welcome");
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