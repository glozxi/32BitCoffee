using System.Collections.Generic;
using System.Linq;
using BrewingData;

public class Order
{
    private Drinks _drink;

    private OrderTypes _type;

    private static readonly Order _emptyOrder = new Order(Drinks.None, OrderTypes.None);
    public static Order EmptyOrder
    {
        get => _emptyOrder;
    }

    public Order(Drinks drink, OrderTypes type)
    {
        _drink = drink;
        _type = type;
    }

    public bool MatchDrink(List<Ingredients> actualIngredients)
    {
        return Recipes.GetRecipe(_drink).SequenceEqual(actualIngredients);
    }

    public float GetPrice()
    {
        if (_type == OrderTypes.Needed || _type == OrderTypes.Wanted)
        {
            return Recipes.GetPrice(_drink);
        }
        return 0f;
    }

}
