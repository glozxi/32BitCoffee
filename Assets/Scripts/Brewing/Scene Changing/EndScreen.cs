using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    TMP_Text _outcome;

    private void OnEnable()
    {
        LevelCash levelCash = FindObjectOfType<LevelCash>();
        _outcome.text = levelCash.IsGoalReached() ? "Success" : "Fail";
    }
}
