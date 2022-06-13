using UnityEngine;

public class DragItem : MonoBehaviour
{
    // Called every frame mouse is down
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);  
    }
    
}
