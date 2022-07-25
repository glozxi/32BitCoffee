using UnityEngine;
using System;
using System.Linq;

public class WaterDispenser : MonoBehaviour
{
    [SerializeField]
    private float _time;
    [SerializeField]
    private Collider2D buttonCollider;
    private bool _isButtonPressed = false;
    [SerializeField]
    private Timer _timer;
    private Cup _cup;
    [SerializeField]
    private IngredientScriptableObject _water;

    private bool _isDispensing = false;

    private void OnEnable()
    {
        _timer.Duration = _time;
        _timer.TimeEnd += EndDispense;

    }

    private void Start()
    {
        foreach (var item in State.Instance.Upgrades.OfType<WaterDispenserTimeUpgrade>().ToList())
        {
            item.UseUpgrade(_timer);
        }
    }

    private void OnDisable()
    {
        _timer.TimeEnd -= EndDispense;
    }
    private void Update()
    {
        if (_isDispensing)
        {
            if (!CupUnder())
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
        _isDispensing = true;
        _cup = CupUnder();
        if (_cup == null)
        {
            print(" no cup under.");
            return;
        }
        print("start dispense");
        _timer.StartTime();

    }

    private Cup CupUnder()
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
        _cup.Add(_water);
    }

    private void InterruptDispense()
    {
        _isDispensing = false;
        _timer.ResetTime();
    }
}
