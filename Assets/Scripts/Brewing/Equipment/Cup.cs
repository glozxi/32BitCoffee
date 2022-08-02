using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Cup : MonoBehaviour, IBinnable
{
    private const string INGREDIENT_TAG_A = "MachineIngredient";
    private const string INGREDIENT_TAG_B = "HybridIngredient";
    private const string INGREDIENT_TAG_ARR = "Ingredient";
    private const string MILK_CUP_TAG = "MilkCup";
    private const int MAX_CONTENT = 4;
    private const string CUP_CONTENT_TAG = "cupContent";

    private bool _isMilkCupStillOverlapping = false;
    private MilkCup[] _milkCups;

    public Vector2 SpawnPosition
    { get; set; }

    private List<IngredientScriptableObject> _contents = new();
    public List<IngredientScriptableObject> Contents
    {
        get => _contents;
    }

    [SerializeField]
    private GameObject _cupContent;

    [SerializeField]
    private Order _order;

    private void Start()
    {
        _milkCups = FindObjectsOfType<MilkCup>();
        SpawnPosition = transform.position;
        Clear();
    }

    private void Update()
    {

        if (!IsMilkCupOverlapping())
        {
            _isMilkCupStillOverlapping = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            if (!Array.Exists(hits, hit => hit.collider == GetComponent<Collider2D>()))
            {
                return;
            }

            if (IsMilkCupOverlapping())
            {
                if (!_isMilkCupStillOverlapping)
                {
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.gameObject.CompareTag(MILK_CUP_TAG))
                        {
                            AddMilk(hit.collider.GetComponent<MilkCup>());
                            _isMilkCupStillOverlapping = true;
                            break;

                        }
                    }
                }
            }
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag(INGREDIENT_TAG_A) || hit.collider.gameObject.CompareTag(INGREDIENT_TAG_B))
                {
                    Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                    Add(ingredient.IngScriptable);
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
        }
    }

    public void ResetCup()
    {
        var newcup = Instantiate(gameObject, SpawnPosition, Quaternion.identity);
        newcup.transform.parent = transform.parent;
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
    public void Add(IngredientScriptableObject ingredientType)
    {
        if (_contents.Count < MAX_CONTENT)
        {
            _contents.Add(ingredientType);
            DisplayContent(ingredientType);
        }
        
    }

    // Display additional cup content
    private void DisplayContent(IngredientScriptableObject ingredientType)
    {
        InstantiateContent(ingredientType);
    }

    private void InstantiateContent(IngredientScriptableObject ingredientType)
    {
        SpriteRenderer contentSpriteRenderer = _cupContent.GetComponent<SpriteRenderer>();
        contentSpriteRenderer.color =  ingredientType.ContentColor;

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
        newContent.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    private void AddMilk(MilkCup milkCup)
    {
        print("add");
        IngredientScriptableObject ing = milkCup.Remove();
        if (ing != null) Add(ing);
    }

    private bool IsMilkCupOverlapping()
    {
        return _milkCups.Any(m => m.GetComponent<Collider2D>().bounds.Intersects(GetComponent<Collider2D>().bounds));
    }
}
