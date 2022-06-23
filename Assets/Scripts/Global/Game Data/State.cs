using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private static State instance;

    public static string NextBrewLevel
    { get; set; }
    public static int Outcome
    { get; set; }

    public static string InkStoryState
    { get; set; }
    public static string TextLog
    { get; set; }

    private static Points _points;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void ContinueStory()
    {
        instance.StartCoroutine(instance.DialogueScene());
    }

    private IEnumerator DialogueScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
        yield return null;
        InkManager inkManager = (InkManager) FindObjectOfType(typeof(InkManager));
        inkManager.LoadStateFromBrew(InkStoryState, TextLog);
    }
}
