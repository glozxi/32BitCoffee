using UnityEngine;

public class Servebox : MonoBehaviour
{
    public delegate void Collision(Cup cup);
    public event Collision CupCollision;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        Cup cup = collidedObject.GetComponent<Cup>();
        CupCollision?.Invoke(cup);
        collidedObject.SetActive(false);
    }
}
