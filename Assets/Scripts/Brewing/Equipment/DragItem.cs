using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class DragItem : MonoBehaviour
{
    private bool _isDragging = false;
    private static GameObject _selectedObject;

    private void Update()
    {
        if (PointerIsOverUI(Input.mousePosition)) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject == null)
            {
                AudioManager.instance.PlaySFX("pickup"); // figure how to selectively do this or tie audio to object variable and read from there.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
                Array.Sort(hits, CompareHits);
                SelectObject(hits);
            }                
        }
        
        if (_isDragging && _selectedObject == gameObject)
        {
            Vector2 pos = MousePos();
            _selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _selectedObject = null;
        }

        ClampPosition();
    }

    private Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }


    private bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    private GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    private PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };

    private void SelectObject(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<DragItem>() != null)
            {
                _selectedObject = hit.collider.gameObject;
                _selectedObject.GetComponent<DragItem>()._isDragging = true;
                break;
            }
        }
    }

    // Sort hits based on layer order, if same layer, then by sorting order
    private int CompareHits(RaycastHit2D x, RaycastHit2D y)
    {
        // Sort by sorting layer, then order in sorting layer.
        // x < y (return -1) if x is x is on a higher layer, so xLayer > yLayer
        int xLayer = SortingLayer.GetLayerValueFromID(x.collider.gameObject.GetComponent<SpriteRenderer>().sortingLayerID);
        int yLayer = SortingLayer.GetLayerValueFromID(y.collider.gameObject.GetComponent<SpriteRenderer>().sortingLayerID);
        if (xLayer > yLayer) return -1;
        else if (xLayer < yLayer) return 1;

        int xOrder = x.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        int yOrder = y.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        if (xOrder < yOrder) return 1;
        else if (xOrder == yOrder) return 0;
        else return -1;
    }
}
