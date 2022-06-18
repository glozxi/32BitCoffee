using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragItem : MonoBehaviour
{
    public int IgnoreLayer = 2;

    private bool _isDragging;
    private GameObject _selectedObject;

    private void Start()
    {
        _isDragging = false;
    }

    private void Update()
    {

        if (PointerIsOverUI(Input.mousePosition)) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == GetComponent<Collider2D>())
                {
                    _selectedObject = hit.collider.gameObject;
                    _isDragging = true;
                    break;
                }
            }
            
        }
        
        if (_isDragging)
        {
            Vector2 pos = MousePos();
            _selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
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
