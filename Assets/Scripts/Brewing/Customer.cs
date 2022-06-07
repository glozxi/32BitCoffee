using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private Recipes.Drinks _wantedDrink;
    private Order _wantedOrder;

    [SerializeField]
    private Recipes.Drinks _neededDrink;
    private Order _neededOrder;

    [SerializeField]
    private Recipes.Drinks _dislikedDrink;
    private Order _dislikedOrder;

    [SerializeField]
    private OrderName _orderName;

    private Collider2D _collider;

    private bool _isServed = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _wantedOrder = new Order(_wantedDrink);
        _neededOrder = new Order(_neededDrink);
        _dislikedOrder = new Order(_dislikedDrink);

        _orderName.Wanted = _wantedDrink;
        _orderName.Needed = _neededDrink;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isServed)
        {
            return;
        }

        if (_collider.bounds.Contains(collision.bounds.max) 
            && _collider.bounds.Contains(collision.bounds.min))
        {
            GameObject collidedObject = collision.gameObject;
            Cup cup = collidedObject.GetComponent<Cup>();
            CheckDrink(cup.Contents);
            _isServed = true;
            Destroy(collidedObject);

        }
    }

     private void OnMouseDown()
    {
        _orderName.ToggleOrder();
    }

    private void CheckDrink(List<Recipes.Ingredients> contents)
    {
        if (_wantedOrder.MatchDrink(contents))
        {
            print("wanted");
            return;
        }
        if (_neededOrder.MatchDrink(contents))
        {
            print("needed");
            return;
        }
        if (_dislikedOrder.MatchDrink(contents))
        {
            print("disliked");
            return;
        }
        print("none");
    }

    internal void Serve(List<GameObject> contents, string drinkMade)
    {
        throw new NotImplementedException();
    }
}