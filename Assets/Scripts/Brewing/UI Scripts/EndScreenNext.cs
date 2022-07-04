using UnityEngine;
using UnityEngine.UI;

public class EndScreenNext : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(() => TaskOnClick());
    }

    private void TaskOnClick()
    {
        Points points = FindObjectOfType<Points>();
        LevelCash levelCash = FindObjectOfType<LevelCash>();
        points.AddCash(levelCash.CurrentCash);
        FindObjectOfType<State>().ContinueStory();
    }


}
