using System.Collections;
using UnityEngine;

public class State : MonoBehaviour
{
    private static State _instance;

    public static string NextBrewLevel
    { get; set; }
    public static int Outcome
    { get; set; }
    public static string Drink
    { get; set; }
    public static string InkStoryState
    { get; set; }
    public static string TextLog
    { get; set; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public static void ContinueStory()
    {
        _instance.StartCoroutine(_instance.DialogueScene());
    }

    private IEnumerator DialogueScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
        yield return null;
        InkManager inkManager = (InkManager) FindObjectOfType(typeof(InkManager));
        inkManager.LoadStateFromBrew(InkStoryState, TextLog);
    }

}
