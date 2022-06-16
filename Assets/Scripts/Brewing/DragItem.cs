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
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider == GetComponent<Collider2D>())
            {
                _selectedObject = hit.collider.gameObject;
                _isDragging = true;
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
