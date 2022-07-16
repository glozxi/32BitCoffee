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
    [SerializeField]
    private Timer _timer;
    private Cup _cup;

    private bool _isDispensing = false;

    private void OnEnable()
    {
        _timer.TimeEnd += EndDispense;
    }
    private void OnDisable()
    {
        _timer.TimeEnd -= EndDispense;
    }
    private void Update()
    {
        if (_isDispensing)
        {
            if (!IsCupUnder())
            {
                InterruptDispense();
                return;
            }
        }
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
                StartDispense();
            }
        }
    }

    private void StartDispense()
    {
        print("start dispense");
        _isDispensing = true;
        _cup = IsCupUnder();
        if (_cup == null)
        {
            print(" no cup under.");
            return;
        }
        if (!_machine.HasIngredient())
        {
            print("nothing in machine");
            return;
        }
        _timer.StartTime();
        
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

    private void EndDispense()
    {
        _isDispensing = false;
        _timer.ResetTime();
        _cup.Add(_machine.DispenseContent());
    }

    private void InterruptDispense()
    {
        _isDispensing = false;
        _timer.ResetTime();
    }
}
