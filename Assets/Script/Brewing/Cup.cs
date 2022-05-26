using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public List<GameObject> contents = new List<GameObject>();

    private string drinkMade;
    private Customer customer;

    void clear()
    {
        contents.Clear();
    }

    void reward()
    {
        customer.serve(contents, drinkMade);
        drinkMade = "";
        clear();
    }

    void add(GameObject item)
    {
        contents.Add(item);
        display_content();
    }

    void display_content()
    {

    }
}
