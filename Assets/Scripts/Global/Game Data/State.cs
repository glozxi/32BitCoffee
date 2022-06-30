using System.Collections;
using UnityEngine;

// Combine with SaveData and GameStateManager?
public class State : MonoBehaviour
{
    public string NextBrewLevel
    { get; set; }
    public int Outcome
    { get; set; }
    public string Drink
    { get; set; }
    public string InkStoryState
    { get; set; }
    public string TextLog
    { get; set; }

    // Move methods to GameStateManager?
    public void ContinueStory()
    {
        StartCoroutine(DialogueScene());
    }

    private IEnumerator DialogueScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
        yield return null;
        InkManager inkManager = (InkManager) FindObjectOfType(typeof(InkManager));
        inkManager.LoadStateFromBrew(InkStoryState, TextLog);
    }

}
