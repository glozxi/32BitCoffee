using System;
using System.Collections.Generic;
using UnityEngine;

// Can only put milk inside, after steaming,
// content becomes steamed milk, same amount as
// original amount of milk.
public class MilkCup : MonoBehaviour, IBinnable
{
    [SerializeField]
    private IngredientScriptableObject _milk;
    [SerializeField]
    private IngredientScriptableObject _steamedMilk;

    private const int MAX_CONTENT = 4;
    private const string CUP_CONTENT_TAG = "cupContent";


    private Stack<IngredientScriptableObject> _contents = new();
    public int ContentCount
    { get => _contents.Count; }

    [SerializeField]
    private GameObject _cupContentPrefab;
    private Stack<GameObject> _cupContentsObj = new();

    private void Start()
    {
        Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            if (!Array.Exists(hits, hit => hit.collider == GetComponent<Collider2D>()))
            {
                return;
            }

            foreach (RaycastHit2D hit in hits)
            {
                // Can only put milk
                IngredientScriptableObject ing = hit.collider.gameObject.GetComponent<Ingredient>()?.IngScriptable;
                if (ing == _milk)
                {
                    Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                    Add(ingredient.IngScriptable);
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
        }
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
    private void Add(IngredientScriptableObject ingredientType)
    {
        if (_contents.Count < MAX_CONTENT)
        {
            _contents.Push(ingredientType);
            DisplayContent(ingredientType);
        }

    }

    public IngredientScriptableObject Remove()
    {
        try
        {
            Destroy(_cupContentsObj.Pop());
            return _contents.Pop();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    // Display additional cup content
    private void DisplayContent(IngredientScriptableObject ingredientType)
    {
        InstantiateContent(ingredientType);
    }

    private void InstantiateContent(IngredientScriptableObject ingredientType)
    {
        SpriteRenderer contentSpriteRenderer = _cupContentPrefab.GetComponent<SpriteRenderer>();
        contentSpriteRenderer.color = ingredientType.ContentColor;

        // Finding position of new content
        float contentHeight = _cupContentPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        float yPos =
            transform.position.y - contentHeight
            + contentHeight * (_contents.Count - 1);
        GameObject newContent = Instantiate(_cupContentPrefab,
            new Vector3(
                transform.position.x,
                yPos,
                0),
            Quaternion.identity);
        newContent.transform.parent = gameObject.transform;
        newContent.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

        _cupContentsObj.Push(newContent);
    }

    // Replace milk with steamed milk
    public void Steam()
    {
        int milkCount = ContentCount;
        Clear();
        for (int i = 0; i < milkCount; i++)
        {
            Add(_steamedMilk);
        }

    }

}
