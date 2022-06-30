using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousSceneChanger : MonoBehaviour
{
    public string PreviousScene
    { get; set; }

    public void LoadPreviousScene()
    {
        State state = FindObjectOfType<State>();
        if (PreviousScene == "DialogueScene")
        {
            InkManager.LoadState(state.InkStoryState, state.TextLog);
        }
        SceneManager.LoadScene(PreviousScene);
    }
}
