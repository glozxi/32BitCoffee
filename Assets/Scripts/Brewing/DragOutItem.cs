using UnityEngine;

// Class of item that can be dragged, creating a new sprite
public abstract class DragOutItem : MonoBehaviour
{
    [SerializeField]
    private Sprite _newSprite;

    private void OnMouseDown()
    {
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.name = this.name;
        GetComponent<SpriteRenderer>().sprite = _newSprite;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    private void OnMouseUp()
    {
        Destroy(gameObject);
    }
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }
}
