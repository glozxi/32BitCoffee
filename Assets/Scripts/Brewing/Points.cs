using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _pointsText;
    private int _points = 0;

    public void UpdatePoints()
    {
        _points++;
        _pointsText.text = _points.ToString();
    }
}
