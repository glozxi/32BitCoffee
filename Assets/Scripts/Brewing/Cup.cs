using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private string INGREDIENT_TAG = "Ingredient";
    public List<Ingredient> contents = new List<Ingredient>();

    private string drinkMade;
    private Customer customer;

    private string CUP_CONTENT_TAG = "cupContent";

    [SerializeField]
    private GameObject cupContent;

    private int MAX_CONTENT = 4;

    [SerializeField]
    private Points points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == INGREDIENT_TAG)
        {
            Ingredient ingredient = collidedObject.GetComponent<Ingredient>();
            
            Add(ingredient);
            Destroy(collidedObject);
            
        }
        
    }

    // Clears contents and displayed cup contents
    void Clear()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(CUP_CONTENT_TAG))
        {
            Destroy(obj);
        }
        contents.Clear();
    }

    void reward()
    {
        // customer.serve(contents, drinkMade);
        // drinkMade = "";
        points.UpdatePoints(1);
        Clear();
    }

    // Adds an item to the cup and displays it
    void Add(Ingredient ingredient)
    {
        /*
        if (contents.Count + 1 > MAX_CONTENT)
        {
            Debug.Log("Cup is now overflowing.");
            return;
        }
        */

        contents.Add(ingredient);
        DisplayContent(ingredient);
        if (contents.Count == MAX_CONTENT)
        {
            reward();
        }
    }

    // Shows what it inside
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
                    case "Milo":
                        InstantiateContent(Color.blue);
                        break;
                }
                break;
        }
        
    }

    void InstantiateContent(Color color)
    {
        SpriteRenderer contentSpriteRenderer = cupContent.gameObject.GetComponent<SpriteRenderer>();
        contentSpriteRenderer.color = color;
        float contentHeight = cupContent.GetComponent<SpriteRenderer>().bounds.size.y;
        float yPos = cupContent.transform.position.y 
            + contentHeight * (contents.Count - 1);
        GameObject newContent = Instantiate(cupContent,
            new Vector3(
                cupContent.transform.position.x,
                yPos,
                0),
            Quaternion.identity);
        newContent.transform.parent = gameObject.transform;
    }
}
