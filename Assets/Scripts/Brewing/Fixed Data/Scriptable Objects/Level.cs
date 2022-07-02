using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private string _levelName;
    public string LevelName
    { get => _levelName; }

    [SerializeField]
    private List<CustomerData> _customerDatas;
    public List<CustomerData> CustomerDatas
    { get => _customerDatas; }

    [SerializeField]
    private float _cashGoal;
    public float CashGoal
    { get => _cashGoal; }
}
