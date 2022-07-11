using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradePrefab;
    [SerializeField]
    private GameObject _upgradeGrid;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private Button _buyButton;
    private Upgrade _chosenUpgrade;
    private GameObject _clickedUpgradeObj;

    private void OnEnable()
    {
        RefreshUpgrades();
        UpgradeInShop.UpgradeClicked += OnUpgradeClicked;
        _buyButton.onClick.AddListener(Buy);
    }
    private void OnDisable()
    {
        UpgradeInShop.UpgradeClicked -= OnUpgradeClicked;
    }

    private void RefreshUpgrades()
    {
        _chosenUpgrade = null;
        _clickedUpgradeObj = null;
        ClearDisplays();
        InstantiatePrefabs();
        InactiveButtonIfNoUpgrades();
    }

    private void ClearDisplays()
    {
        foreach (Transform child in _upgradeGrid.transform)
        {
            Destroy(child.transform.gameObject);
        }
        _name.text = "";
        _description.text = "";
    }

    private void InstantiatePrefabs()
    {
        foreach (Upgrade val in UpgradesData.Data.Values)
        {
            if (!State.Instance.Upgrades.Contains(val))
            {
                GameObject obj = Instantiate(_upgradePrefab, _upgradeGrid.transform);
                obj.GetComponent<UpgradeInShop>().Upgrade = val;
            }
        }
    }

    private void InactiveButtonIfNoUpgrades()
    {
        // Checks if there are any upgrades in State's list that are not in UpgradesData's list
        if (!UpgradesData.Data.Values.Except(State.Instance.Upgrades).Any())
        {
            _buyButton.transform.gameObject.SetActive(false);
        }
    }

    private void OnUpgradeClicked(GameObject obj, Upgrade upgrade)
    {
        _name.text = upgrade.Name;
        _description.text = upgrade.Description;
        _chosenUpgrade = upgrade;
        _clickedUpgradeObj = obj;
    }

    // Buy using network points
    // Add to current upgrades, subtract points, disable in shop
    private void Buy()
    {
        Points points = FindObjectOfType<Points>();
        if (points.NetworkPoints < _chosenUpgrade.Cost)
        {
            print($"Not enough network points, you have {points.NetworkPoints}, {_chosenUpgrade.Cost} required.");
            return;
        }
        State.Instance.Upgrades.Add(_chosenUpgrade);
        points.RemoveAnalysePoints(_chosenUpgrade.Cost);
        RefreshUpgrades();
    }
}
