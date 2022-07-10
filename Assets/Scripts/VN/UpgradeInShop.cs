using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInShop : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _priceText;
    [SerializeField]
    private TMP_Text _name;

    private Upgrade _upgrade;
    public Upgrade Upgrade
    {
        get => _upgrade;
        set
        {
            _upgrade = value;
            Display();
        }
    }

    private void Display()
    {
        _priceText.text = "$" + Upgrade.Cost.ToString();
        _name.text = Upgrade.Name;
    }
}
