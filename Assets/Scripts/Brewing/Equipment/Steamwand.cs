using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Press the button to start steam.
// Only starts if milk cup touches collider and there is milk inside the cup.
// Stops if milk cup moved outside collider.
public class Steamwand : MonoBehaviour
{
    [SerializeField]
    private float _time;
    [SerializeField]
    private Collider2D buttonCollider;
    private bool _isButtonPressed = false;
    [SerializeField]
    private Timer _timer;
    [SerializeField]
    private List<GameObject> _milkCups;
    // Current milk cup
    private MilkCup _milkCup;
    RaycastHit2D[] _hits;

    private bool _isSteaming = false;

    private void OnEnable()
    {
        _timer.Duration = _time;
        _timer.TimeEnd += EndSteam;
    }
    private void OnDisable()
    {
        _timer.TimeEnd -= EndSteam;
    }
    private void Update()
    {
        if (_isSteaming)
        {
            if (!CupUnder())
            {
                InterruptSteam();
                return;
            }
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
        ButtonClick(_hits);
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
                StartSteam();
            }
        }
    }

    private void StartSteam()
    {
        _isSteaming = true;
        _milkCup = CupUnder();
        if (_milkCup == null)
        {
            print(" no cup under.");
            return;
        }
        if (_milkCup.ContentCount == 0)
        {
            print("no milk.");
            return;
        }
        print("start steam");
        _timer.StartTime();

    }

    private MilkCup CupUnder()
    {
        foreach (var milkCup in _milkCups)
        {
            if (GetComponent<Collider2D>().bounds.Intersects(milkCup.GetComponent<Collider2D>().bounds))
            {
                return milkCup.GetComponent<MilkCup>();
            }
        }
        return null;
    }

    private void EndSteam()
    {
        _isSteaming = false;
        _timer.ResetTime();
        _milkCup.Steam();
    }

    private void InterruptSteam()
    {
        _isSteaming = false;
        _timer.ResetTime();
    }
}
