using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Displays customers
public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _customerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCustomer();
    }

    private void DisplayCustomer()
    {
        Customer customer = _customerGameObject.GetComponent<Customer>();
        customer.SetDrinks(Recipes.Drinks.Latte, Recipes.Drinks.Chocolate, Recipes.Drinks.ChocoLatte);
        _customerGameObject.SetActive(true);
    }
}
