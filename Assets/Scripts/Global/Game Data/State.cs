using System.Collections;
using System.Collections.Generic;
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
    public string BGMFile
    { get; set; }
    public List<CharData> CharDatas
    { get; set; } = new();

    [SerializeField]
    private List<Upgrade> _upgrades = new();
    public List<Upgrade> Upgrades
    {
        get => _upgrades;
        set => _upgrades = value;
    }

    public static State Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Move methods to GameStateManager?
    public void ContinueStory()
    {
        StartCoroutine(DialogueScene());
    }

    private IEnumerator DialogueScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
        yield return null;
        InkManager inkManager = (InkManager)FindObjectOfType(typeof(InkManager));
        inkManager.LoadStateFromBrew(InkStoryState, TextLog);
    }

}