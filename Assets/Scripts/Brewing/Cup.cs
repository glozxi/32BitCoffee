using System.Collections.Generic;
using UnityEngine;

public class Cup : DragItem
{
    private const string IngredientTag = "Ingredient";
    private const int MaxContent = 4;
    private const string CupContentTag = "cupContent";

    private List<Recipes.Ingredients> _contents = new();

    [SerializeField]
    private GameObject _cupContent;

    [SerializeField]
    private Order _order;

    // private string drinkMade;
    // private Customer customer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag(IngredientTag))
        {
            Ingredient ingredient = collidedObject.GetComponent<Ingredient>();
            
            Add(ingredient);
            Destroy(collidedObject);
        }
        
    }

    // Clears contents and displayed cup contents
    private void Clear()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(CupContentTag))
        {
            Destroy(obj);
        }
        _contents.Clear();
    }

    // Adds an item to the cup and displays it
    private void Add(Ingredient ingredient)
    {
        _contents.Add(ingredient.IngredientType);
        DisplayContent(ingredient);
        if (_contents.Count == MaxContent)
        {
            Reward();
        }
    }

    // Rewards player
    private void Reward()
    {
        // customer.serve(contents, drinkMade);
        // drinkMade = "";
        _order.MatchDrink(_contents);
        
        Clear();
    }

    // Display additional cup content
    private void DisplayContent(Ingredient ingredient)
    {
        InstantiateContent(ingredient);

    }

    private void InstantiateContent(Ingredient ingredient)
    {
        SpriteRenderer contentSpriteRenderer = _cupContent.GetComponent<SpriteRenderer>();
        contentSpriteRenderer.color = ingredient.ContentColor;

        // Finding position of new content
        float contentHeight = _cupContent.GetComponent<SpriteRenderer>().bounds.size.y;
        float yPos =
            transform.position.y - contentHeight
            + contentHeight * (_contents.Count - 1);
        GameObject newContent = Instantiate(_cupContent,
            new Vector3(
                transform.position.x,
                yPos,
                0),
            Quaternion.identity);
        newContent.transform.parent = gameObject.transform;
    }
}
