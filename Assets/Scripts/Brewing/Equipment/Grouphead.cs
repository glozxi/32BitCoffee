using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grouphead : MonoBehaviour
{
    [SerializeField]
    private Machine _machine;
    [SerializeField]
    private Collider2D buttonCollider;
    private bool _isButtonPressed = false;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
        ButtonClick(hits);
    }


    private void ButtonClick(RaycastHit2D[] hits)
    {
        // Pointer out of button
        if (!Array.Exists(hits, hit => hit.collider == buttonCollider))
        {
            _isButtonPressed = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Array.Exists(hits, hit => hit.collider == buttonCollider))
            {
                _isButtonPressed = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Button clicked
            if (Array.Exists(hits, hit => hit.collider == buttonCollider) && _isButtonPressed)
            {
                _isButtonPressed = false;
                Dispense();
            }
        }
    }

    private void Dispense()
    {
        print("dispense");
        Cup cup = IsCupUnder();
        if (cup != null)
        {
            cup.Add(_machine.DispenseContent());
        }
    }

    private Cup IsCupUnder()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, Mathf.Infinity);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<Cup>() != null)
            {
                return hit.collider.GetComponent<Cup>();
            }
        }
        return null;
    }
}
