using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerIngredient : MonoBehaviour
{
    public Sprite newSprite;

    // Start is called before the first frame update

    public void OnMouseDown()
    {
        GameObject clone = Instantiate(gameObject, this.transform.position, this.transform.rotation);
        clone.name = this.name;
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
        this.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    public void OnMouseUp()
    {
        Destroy(this.gameObject);
    }
    public void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }
}
