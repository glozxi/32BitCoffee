using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BrewingData;

public class EditModeTests
{
    // Order tests
    [Test]
    public void ChocolateDrinkMatchChocoLatte()
    {
        Order order = new Order(Drinks.ChocoLatte, OrderTypes.None);
        bool result = order.DoesDrinkMatch(new List<Ingredients> { 
            Ingredients.Chocolate, Ingredients.Chocolate, Ingredients.Chocolate, Ingredients.Chocolate 
        });
        Assert.AreEqual(false, result);
    }

    [Test]
    public void ChocoLatteDrinkMatchChocoLatte()
    {
        Order order = new Order(Drinks.ChocoLatte, OrderTypes.None);
        bool result = order.DoesDrinkMatch(new List<Ingredients> {
            Ingredients.Chocolate, Ingredients.Chocolate, Ingredients.Espresso, Ingredients.Espresso
        });
        Assert.AreEqual(true, result);
    }

    [Test]
    public void ThreeIngredientDrinkMatchChocoLatte()
    {
        Order order = new Order(Drinks.ChocoLatte, OrderTypes.None);
        bool result = order.DoesDrinkMatch(new List<Ingredients> {
            Ingredients.Chocolate, Ingredients.Chocolate, Ingredients.Espresso
        });
        Assert.AreEqual(false, result);
    }

    [Test]
    public void PriceOfNeededChocolate()
    {
        Order order = new Order(Drinks.Chocolate, OrderTypes.Needed);
        float price = order.GetPrice();
        Assert.AreEqual(Recipes.GetPrice(Drinks.Chocolate), price);
    }

    [Test]
    public void PriceOfDislikedChocolate()
    {
        Order order = new Order(Drinks.Chocolate, OrderTypes.Disliked);
        float price = order.GetPrice();
        Assert.AreEqual(0, price);
    }

    [Test]
    public void PriceOfWantedChocolate()
    {
        Order order = new Order(Drinks.Chocolate, OrderTypes.Wanted);
        float price = order.GetPrice();
        Assert.AreEqual(Recipes.GetPrice(Drinks.Chocolate), price);
    }
}
