using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _outcome;
    [SerializeField]
    private GameObject _retry;
    [SerializeField]
    private GameObject _next;

    private void OnEnable()
    {
        LevelCash levelCash = FindObjectOfType<LevelCash>();
        if (levelCash.IsGoalReached())
        {
            _outcome.text = "Success";
            _retry.SetActive(false);
            _next.SetActive(true);
        }
        else
        {
            _outcome.text = "Fail";
            _retry.SetActive(true);
            _next.SetActive(false); ;
        }
         
    }
}
