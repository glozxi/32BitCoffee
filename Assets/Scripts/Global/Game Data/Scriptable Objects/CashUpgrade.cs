using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Cash Upgrade")]
public class CashUpgrade : Upgrade
{
    // Add if oder is needed or wanted
    [SerializeField]
    private float _addedCash;
    public float AddedCash
    { get => _addedCash; }

    [SerializeField]
    private float _cashScale = 1;
    public float CashScale
    { get => _cashScale; }

    private void Reset()
    {
        _addedCash = 0;
        _cashScale = 1;
    }

    public void UseUpgrade(Order order)
    {
        order.AddedCash += _addedCash;
        order.CashScale *= _cashScale;
    }
}
