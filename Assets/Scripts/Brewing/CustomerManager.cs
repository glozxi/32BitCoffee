using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Brewing;
using System;

// Displays customers
public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _customerGameObject;
    private Queue<CustomerData> _queue;
    private Customer _customer;

    // Start is called before the first frame update
    void Start()
    {
        _queue = CustomersData.GetQueue(1);
        _customer = _customerGameObject.GetComponent<Customer>();
        _customer.CustomerServed += OnCustomerServed;

        DisplayNextCustomer();
    }

    private void OnDisable()
    {
        _customer.CustomerServed -= OnCustomerServed;
    }


    private void OnCustomerServed()
    {
        // _customerGameObject.SetActive(false);
        DisplayNextCustomer();
    }

    private void DisplayCustomer(CustomerData customerData)
    {
        _customer.SetDrinks(customerData.Wanted, customerData.Needed, customerData.Disliked);
        _customerGameObject.SetActive(true);
    }

    private CustomerData NextCustomer()
    {
        return _queue.Dequeue();
    }

    private void DisplayNextCustomer()
    {
        try
        {
            DisplayCustomer(NextCustomer());
        }
        catch (InvalidOperationException)
        {
            print("No more customers.");
            _customerGameObject.SetActive(false);
        }
    }
}
