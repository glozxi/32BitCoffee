using System.Collections.Generic;
using UnityEngine;
using BrewingData;

public class Cup : DragItem
{
    private const string INGREDIENT_TAG = "Ingredient";
    private const int MAX_CONTENT = 4;
    private const string CUP_CONTENT_TAG = "cupContent";

    public Vector2 SpawnPosition
    { get; set; }

    private List<Ingredients> _contents = new();
    public List<Ingredients> Contents
    {
        get => _contents;
    }

    [SerializeField]
    private GameObject _cupContent;

    [SerializeField]
    private Order _order;

    private void Start()
    {
        SpawnPosition = transform.position;
        Clear();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag(INGREDIENT_TAG))
        {
            Ingredient ingredient = collidedObject.GetComponent<Ingredient>();
            
            Add(ingredient);
            Destroy(collidedObject);
        }
        
    }

    public void ResetCup()
    {
        var newcup = Instantiate(gameObject, SpawnPosition, Quaternion.identity);
        newcup.transform.parent = this.transform.parent;
        Destroy(gameObject);
    }


    // Clears contents and displayed cup contents
    public void Clear()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(CUP_CONTENT_TAG))
            {
                Destroy(child.gameObject);
            }
        }
        _contents.Clear();
    }

    // Adds an item to the cup if possible and displays it
    private void Add(Ingredient ingredient)
    {
        if (_contents.Count < MAX_CONTENT)
        {
            _contents.Add(ingredient.IngredientType);
            DisplayContent(ingredient);
        }
        
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
