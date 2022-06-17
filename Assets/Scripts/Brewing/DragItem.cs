using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour
{
    private bool _isDragging;
    private GameObject _selectedObject;

    private void Start()
    {
        _isDragging = false;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
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
    }

    Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }
}
