using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BrewingData;
using System;

public class Machine : MonoBehaviour
{
    private const string INGREDIENT_TAG = "Ingredient";
    private const int MAX_CONTENT = 1;
    private const string CUP_CONTENT_TAG = "cupContent";
    private List<IngredientScriptableObject> _contents = new();
    public List<IngredientScriptableObject> Contents
    {
        get => _contents;
    }

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
                if (hit.collider.gameObject.CompareTag(INGREDIENT_TAG))
                {
                    Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                    Add(ingredient.IngScriptable);
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
        }
        
    }

    // Adds an item if possible and displays it
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

    private void DisplayContent(IngredientScriptableObject ingredient)
    {
        InstantiateContent(ingredient);
    }

    private void InstantiateContent(IngredientScriptableObject ingredient)
    {
        
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
}
