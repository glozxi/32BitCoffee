using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        CustomerManager.AllServed += OnAllServed;
    }

    private void OnDisable()
    {
        CustomerManager.AllServed -= OnAllServed;
    }

    private void OnAllServed()
    {
        State.ContinueStory();
    }
}
