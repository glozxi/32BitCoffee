using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Recipes.Drinks _wantedDrink;
    private Order _wantedOrder;

    private Recipes.Drinks _neededDrink;
    private Order _neededOrder;

    private Recipes.Drinks _dislikedDrink;
    private Order _dislikedOrder;

    [SerializeField]
    private WantedOrderText _wantedText;
    [SerializeField]
    private NeededOrderText _neededText;

    [SerializeField]
    private Servebox _servebox;


    private void OnEnable()
    {
        _servebox.CupCollision += OnServed;

    }

    private void OnDisable()
    {
        _servebox.CupCollision -= OnServed;
    }

    public void SetDrinks(Recipes.Drinks wantedDrink, Recipes.Drinks neededDrink, Recipes.Drinks dislikedDrink)
    {
        _wantedDrink = wantedDrink;
        _neededDrink = neededDrink;
        _dislikedDrink = dislikedDrink;
        _wantedOrder = new Order(_wantedDrink);
        _neededOrder = new Order(_neededDrink);
        _dislikedOrder = new Order(_dislikedDrink);

        _wantedText.Drink = _wantedDrink;
        _neededText.Drink = _neededDrink;
    }

    private void OnServed(Cup cup)
    {
        CheckDrink(cup.Contents);
        gameObject.SetActive(false);
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

}