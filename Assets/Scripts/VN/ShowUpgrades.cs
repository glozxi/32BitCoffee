using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpgrades : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradePrefab;

    private void OnEnable()
    {
        RemoveDisplayedUpgrades();
        InstantiatePrefabs();
    }

    private void RemoveDisplayedUpgrades()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.transform.gameObject);
        }
    }

    private void InstantiatePrefabs()
    {
        foreach (Upgrade val in UpgradesData.Data.Values)
        {
            GameObject obj = Instantiate(_upgradePrefab, transform);
            obj.GetComponent<UpgradeInShop>().Upgrade = val;
        }
        
    }
}
