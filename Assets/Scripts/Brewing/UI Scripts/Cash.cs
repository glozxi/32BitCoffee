using UnityEngine;
using TMPro;
public class Cash : MonoBehaviour
{
    private float _cash = 0f;

    [SerializeField]
    private TMP_Text _text;


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
        _cash += order.GetPrice();
        _text.text = _cash.ToString();
    }
}
