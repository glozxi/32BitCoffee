using UnityEngine;
using UnityEngine.UI;

public class EndScreenNext : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(() => FindObjectOfType<State>().ContinueStory());
    }


}
