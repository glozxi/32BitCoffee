using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInShop : MonoBehaviour
{
    public delegate void ButtonClickedEventHandler(GameObject clicked, Upgrade upgrade);
    public static event ButtonClickedEventHandler UpgradeClicked;

    [SerializeField]
    private TMP_Text _priceText;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _image;

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
        _button.onClick.AddListener(() => UpgradeClicked?.Invoke(gameObject, _upgrade));
        if (_upgrade != null)
        {
            _image.sprite = _upgrade.Sprite;
        }
    }
}
