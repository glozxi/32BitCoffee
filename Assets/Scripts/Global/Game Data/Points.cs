using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Points : MonoBehaviour
{
    public delegate void PointsUpdateEventHandler(float amount);
    public static event PointsUpdateEventHandler CashUpdated;
    public static event PointsUpdateEventHandler NetworkPointsUpdated;

    private const float POINTS_TO_ADD = 5;
    public float AddedPoints
    { get; set; } = 0;
    public float PointsScale
    { get; set; } = 1;

    private float _cash = 0f;
    public float Cash
    {
        get => _cash;
    }

    [SerializeField]
    private float _networkPoints = 0f;
    public float NetworkPoints
    {
        get => _networkPoints;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Use upgrades
        ResetUpgrades();
        foreach (var upgrade in State.Instance.Upgrades.OfType<NetworkPointsUpgrade>().ToList())
        {
            upgrade.UseUpgrade(this);
        }
    }

    public void AddCash(float amount)
    {
        _cash += amount;
    }

    public void AddAnalysePoints()
    {
        _networkPoints += POINTS_TO_ADD + AddedPoints;
        NetworkPointsUpdated?.Invoke(_networkPoints);
    }

    public void LoadPoints(float cash, float networkPoints)
    {
        _cash = cash;
        _networkPoints = networkPoints;
    }
    public void ResetPoints()
    {
        _cash = 0f;
        _networkPoints = 0f;
    }

    private void ResetUpgrades()
    {
        AddedPoints = 0;
        PointsScale = 1;
    }

    public void RemoveAnalysePoints(float cost)
    {
        _networkPoints -= cost;
        NetworkPointsUpdated?.Invoke(_networkPoints);
    }
}
