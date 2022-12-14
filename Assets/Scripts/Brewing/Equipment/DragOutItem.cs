using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public abstract class DragOutItem : MonoBehaviour
{
    [SerializeField]
    private Sprite _newSprite;

    protected virtual string DraggedOutTag
    { get; set; }

    private bool _isDragging = false;
    private GameObject _selectedObject;
    [SerializeField]
    private List<string> _dontDestroyIfOver;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);

        if (IsPointerOverUI(Input.mousePosition)) return;

        if (Input.GetMouseButtonDown(0) && !_isDragging)
        {
            if (Array.Exists(hits, hit => hit.collider == GetComponent<Collider2D>()))
            {
                _selectedObject = hit.collider.gameObject;
                _isDragging = true;

                GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
                clone.name = this.name;

                GetComponent<SpriteRenderer>().sprite = _newSprite;
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                transform.tag = DraggedOutTag;
            }
        }

        if (_isDragging)
        {
            Vector2 pos = MousePos();
            _selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            if (!Array.Exists(hits, hit => _dontDestroyIfOver.Contains(hit.collider.gameObject.tag)))
            {
                _isDragging = false;
                Destroy(gameObject);
            }
            
        }
    }

    private Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private bool IsPointerOverUI(Vector2 screenPos)
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
}
