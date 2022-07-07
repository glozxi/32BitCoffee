using UnityEngine;
using System;

public class Servebox : MonoBehaviour
{
    public delegate void Collision(Cup cup);
    public event Collision CupCollision;

    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            if (Array.Exists(hits, hit => hit.collider.gameObject == this)
                && Array.Exists(hits, hit => hit.collider.gameObject.GetComponent<Cup>() != null))
            {
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.GetComponent<Cup>() != null)
                    {
                        CupCollision?.Invoke(hit.collider.gameObject.GetComponent<Cup>());
                        return;
                    }
                }
            }

        }
    }
}
