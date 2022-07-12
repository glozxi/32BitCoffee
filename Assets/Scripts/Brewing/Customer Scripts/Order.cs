using System.Collections.Generic;
using System.Linq;
using BrewingData;

public class Order: IOrder
{
    private Drinks _drink;

    private OrderTypes _type;

    private static readonly Order _emptyOrder = new Order(Drinks.None, OrderTypes.None);
    public static Order EmptyOrder
    {
        get => _emptyOrder;
    }

    public float AddedCash
    { get; set; } = 0;
    public float CashScale
    { get; set; } = 1;

    public Order(Drinks drink, OrderTypes type)
    {
        _drink = drink;
        _type = type;
    }

    public bool DoesDrinkMatch(List<Ingredients> actualIngredients)
    {
        return Recipes.GetRecipe(_drink).SequenceEqual(actualIngredients);
    }

    public float GetPrice()
    {
        ResetUpgrades();
        foreach (var upgrade in State.Instance.Upgrades.OfType<CashUpgrade>().ToList())
        {
            upgrade.UseUpgrade(this);
        }

        if (_type == OrderTypes.Needed || _type == OrderTypes.Wanted)
        {
            return (Recipes.GetPrice(_drink) + AddedCash) * CashScale;
        }
        return 0f;
    }

    private void ResetUpgrades()
    {
        AddedCash = 0;
        CashScale = 1;
    }
}
