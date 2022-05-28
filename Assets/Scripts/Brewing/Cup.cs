using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private const string IngredientTag = "Ingredient";
    private const int MaxContent = 4;
    private const string CupContentTag = "cupContent";

    private List<Ingredient> Contents = new();

    [SerializeField]
    private GameObject _cupContent;

    [SerializeField]
    private Points _points;


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
    void Clear()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(CupContentTag))
        {
            Destroy(obj);
        }
        Contents.Clear();
    }

    // Rewards player
    void Reward()
    {
        // customer.serve(contents, drinkMade);
        // drinkMade = "";
        _points.UpdatePoints();
        Clear();
    }

    // Adds an item to the cup and displays it
    void Add(Ingredient ingredient)
    {
        Contents.Add(ingredient);
        DisplayContent(ingredient);
        if (Contents.Count == MaxContent)
        {
            Reward();
        }
    }

    // Display additional cup content
    void DisplayContent(Ingredient ingredient)
    {
        string ingredientType = ingredient.IngredientType;
        string ingredientName = ingredient.IngredientName;
        // Better to switch to enums
        switch (ingredientType)
        {
            case "Base":
                switch (ingredientName)
                {
                    case "Espresso":
                        InstantiateContent(Color.black);
                        break;
                    case "Chocolate":
                        InstantiateContent(Color.blue);
                        break;
                }
                break;
        }
        
    }

    void InstantiateContent(Color color)
    {
        SpriteRenderer contentSpriteRenderer = _cupContent.GetComponent<SpriteRenderer>();
        contentSpriteRenderer.color = color;

        // Finding position of new content
        float contentHeight = _cupContent.GetComponent<SpriteRenderer>().bounds.size.y;
        float yPos =
            transform.position.y - contentHeight
            + contentHeight * (Contents.Count - 1);
        GameObject newContent = Instantiate(_cupContent,
            new Vector3(
                transform.position.x,
                yPos,
                0),
            Quaternion.identity);
        newContent.transform.parent = gameObject.transform;
    }
}
