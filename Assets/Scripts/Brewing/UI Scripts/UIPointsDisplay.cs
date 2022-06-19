using UnityEngine;
using TMPro;

public abstract class UIPointsDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public void OnAmountUpdate(float amount)
    {
        _text.text = amount.ToString();
    }

}
