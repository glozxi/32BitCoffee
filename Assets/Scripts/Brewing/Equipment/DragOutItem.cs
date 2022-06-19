using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
// Class of item that can be dragged, creating a new sprite
public abstract class DragOutItem : MonoBehaviour
{
    [SerializeField]
    private Sprite _newSprite;

    [SerializeField]
    private string draggedOutTag;

    private bool _isDragging;
    private GameObject _selectedObject;

    private void Start()
    {
        _isDragging = false;
    }

    private void Update()
    {
        if (PointerIsOverUI(Input.mousePosition)) return;

        if (Input.GetMouseButtonDown(0) && !_isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider == GetComponent<Collider2D>())
            {
                _selectedObject = hit.collider.gameObject;
                _isDragging = true;

                GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
                clone.name = this.name;

                GetComponent<SpriteRenderer>().sprite = _newSprite;
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                transform.tag = draggedOutTag;
                
            }
        }

        if (_isDragging)
        {
            Vector2 pos = MousePos();
            _selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _isDragging = false;
            Destroy(gameObject);
        }
    }

    Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    public static bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };
}
