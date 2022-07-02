using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
        }
        
        if (_isDragging && _selectedObject == gameObject)
        {
            Vector2 pos = MousePos();
            _selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            if (_selectedObject == gameObject)
            {
                _selectedObject = null;
            }
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

    
}
