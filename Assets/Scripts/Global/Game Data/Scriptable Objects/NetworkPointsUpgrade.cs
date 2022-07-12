using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "NPoints Upgrade")]
public class NetworkPointsUpgrade : Upgrade
{
    [SerializeField]
    private float _addedNetworkPoints;
    public float AddedNetworkPoints
    { get => _addedNetworkPoints; }

    [SerializeField]
    private float _networkPointsScale = 1;
    public float NetworkPointsScale
    { get => _networkPointsScale; }

    private void Reset()
    {
        _addedNetworkPoints = 0;
        _networkPointsScale = 1;
    }

    public void UseUpgrade(Points points)
    {
        points.AddedPoints += _addedNetworkPoints;
        points.PointsScale *= _networkPointsScale;
    }
}
