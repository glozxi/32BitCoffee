using UnityEngine;
using TMPro;
public class CashDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void OnEnable()
    {
        Cash.CashUpdated += OnCashUpdate;
    }

    private void OnDisable()
    {
        Cash.CashUpdated -= OnCashUpdate;
    }

    public void OnCashUpdate(float amount)
    {
        _text.text = amount.ToString();
        print("updated");
    }

}
