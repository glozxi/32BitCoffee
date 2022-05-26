using UnityEngine;

public class ClickerIngredient : MonoBehaviour
{
    [SerializeField]
    private Sprite newSprite;

    public void OnMouseDown()
    {
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.name = this.name;
        GetComponent<SpriteRenderer>().sprite = newSprite;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    public void OnMouseUp()
    {
        Destroy(gameObject);
    }
    public void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }
}
