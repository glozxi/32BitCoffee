using UnityEngine;

public class Servebox : MonoBehaviour
{
    public delegate void Collision(Cup cup);
    public event Collision CupCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        Cup cup = collidedObject.GetComponent<Cup>();
        CupCollision?.Invoke(cup);
    }
}
