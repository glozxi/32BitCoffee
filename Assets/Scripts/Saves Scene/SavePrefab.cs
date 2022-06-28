using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePrefab : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
