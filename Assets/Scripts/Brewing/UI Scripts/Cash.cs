using UnityEngine;
using Assets.Scripts.Brewing;

public class Cash : MonoBehaviour
{
    private void OnEnable()
    {
        Customer.OrderServed += OnServed;
    }

    private void OnDisable()
    {
        Customer.OrderServed -= OnServed;
    }

    private void OnServed(Order order)
    {

    }
}
