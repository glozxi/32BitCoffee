using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        IBinnable cup = collidedObject.GetComponent<IBinnable>();
        cup.Clear();
    }
}
