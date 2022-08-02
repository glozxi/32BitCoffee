using System.Collections.Generic;
using UnityEngine;
using System;

public class Machine : MonoBehaviour
{
    private const string INGREDIENT_TAG_A = "MachineIngredient";
    private const string INGREDIENT_TAG_B = "HybridIngredient";
    private const int MAX_CONTENT = 1;
    private const string CUP_CONTENT_TAG = "cupContent";
    private List<IngredientScriptableObject> _contents = new();
    public List<IngredientScriptableObject> Contents
    {
        get => _contents;
    }

    [SerializeField]
    private GameObject _content;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
        if (Input.GetMouseButtonUp(0))
        {
            if (!Array.Exists(hits, hit => hit.collider == GetComponent<Collider2D>()))
            {
                return;
            }

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag(INGREDIENT_TAG_A) || hit.collider.gameObject.CompareTag(INGREDIENT_TAG_B))
                {
                    Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();

                    if (ingredient.IngScriptable.IngredientType == "Tea")
                    {
                        IngredientScriptableObject ingredientAlt = Resources.Load("Ingredient/Teaxpresso") as IngredientScriptableObject;
                        Add(ingredientAlt);
                    }
                    else
                    {
                        Add(ingredient.IngScriptable);
                    }
                    
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
        }
        
    }

    // Adds an ingredient if possible
    private void Add(IngredientScriptableObject ingredient)
    {
        if (_contents.Count < MAX_CONTENT)
        {
            _contents.Add(ingredient);
            DisplayContent(ingredient);
        }

    }

    public IngredientScriptableObject DispenseContent()
    {
        return _contents[0];
    }

    public bool HasIngredient()
    {
        return (_contents.Count > 0);
    }

    private void DisplayContent(IngredientScriptableObject ingredient)
    {
        InstantiateContent(ingredient);
    }

    private void InstantiateContent(IngredientScriptableObject ingredient)
    {
        Debug.Log(ingredient.Sprite);
        _content.GetComponent<SpriteRenderer>().sprite = ingredient.Sprite;
        // _content.
    }

    // Clears contents
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
}
