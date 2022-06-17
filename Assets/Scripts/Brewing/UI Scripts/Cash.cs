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

    private void OnServed(Order order, Timer timer)
    {
        _cash += timer.HasBonus ? order.GetPrice() * 2 : order.GetPrice();
        _text.text = _cash.ToString();
    }
}
