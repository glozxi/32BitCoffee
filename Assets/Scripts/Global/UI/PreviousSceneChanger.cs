using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousSceneChanger : MonoBehaviour
{
    private static PreviousSceneChanger _instance;
    public static PreviousSceneChanger Instance 
    { get => _instance; }

    public string PreviousScene
    { get; set; }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadPreviousScene()
    {
        if (PreviousScene == "DialogueScene")
        {
            InkManager.LoadState(State.InkStoryState, State.TextLog);
        }
        SceneManager.LoadScene(PreviousScene);
    }
}
