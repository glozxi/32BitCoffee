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
        AudioManager.instance.PlaySFX("bin"); // figure how to selectively do this or tie audio to object variable and read from there.
    }
}
