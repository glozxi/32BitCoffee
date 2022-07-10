using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowUpgrades : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradePrefab;
    [SerializeField]
    private GameObject _upgradeGrid;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _description;

    private void OnEnable()
    {
        RemoveDisplayedUpgrades();
        InstantiatePrefabs();
        UpgradeInShop.UpgradeClicked += OnUpgradeClicked;
    }
    private void OnDisable()
    {
        UpgradeInShop.UpgradeClicked -= OnUpgradeClicked;
    }

    private void RemoveDisplayedUpgrades()
    {
        foreach (Transform child in _upgradeGrid.transform)
        {
            Destroy(child.transform.gameObject);
        }
    }

    private void InstantiatePrefabs()
    {
        foreach (Upgrade val in UpgradesData.Data.Values)
        {
            GameObject obj = Instantiate(_upgradePrefab, _upgradeGrid.transform);
            obj.GetComponent<UpgradeInShop>().Upgrade = val;
        }
        
    }

    private void OnUpgradeClicked(Upgrade upgrade)
    {
        _name.text = upgrade.Name;
        _description.text = upgrade.Description;
    }
}
