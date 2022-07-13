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
    private GameObject _boughtUpgradePrefab;
    [SerializeField]
    private GameObject _upgradeGrid;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private Button _buyButton;
    private Upgrade _chosenUpgrade;

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
        ClearDisplays();
        InstantiatePrefabs();
    }

    private void ClearDisplays()
    {
        foreach (Transform child in _upgradeGrid.transform)
        {
            Destroy(child.transform.gameObject);
        }
        _buyButton.transform.gameObject.SetActive(false);
        _name.text = "";
        _description.text = "";
    }

    private void InstantiatePrefabs()
    {
        // To display after unbought
        List<Upgrade> bought = new();

        // Display all base upgrades not bought and next level for bought if it exists
        // Display all unbought base
        // Display for bought all upgrade
        foreach (Upgrade val in UpgradesData.Data.Values)
        {
            if (!State.Instance.Upgrades.Contains(val))
            {
                // Display
                if (val.IsBaseUpgrade())
                {
                    GameObject obj = Instantiate(_upgradePrefab, _upgradeGrid.transform);
                    obj.GetComponent<UpgradeInShop>().Upgrade = val;
                }
            }
            else
            {
                // already bought, add to bought list
                bought.Add(val);
                // Display upgrade if possible
                if (val.NextUpgrade != null)
                {
                    GameObject obj = Instantiate(_upgradePrefab, _upgradeGrid.transform);
                    obj.GetComponent<UpgradeInShop>().Upgrade = val.NextUpgrade;
                }
            }
        }

        // Display bought upgrades
        foreach (Upgrade val in bought)
        {
            GameObject obj = Instantiate(_boughtUpgradePrefab, _upgradeGrid.transform);
            obj.GetComponent<UpgradeInShop>().Upgrade = val;
        }
    }

    private bool IsAllUpgradesBought()
    {
        // Checks if there are any upgrades in State's list that are not in UpgradesData's list
        return !UpgradesData.Data.Values.Except(State.Instance.Upgrades).Any();
    }

    private void OnUpgradeClicked(GameObject clickedObj, Upgrade upgrade)
    {
        _name.text = upgrade.Name;
        _description.text = upgrade.Description;
        _chosenUpgrade = upgrade;
        _buyButton.transform.gameObject.SetActive(!State.Instance.Upgrades.Contains(upgrade));
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
