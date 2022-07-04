using UnityEngine;

public class EndScreenOpener : MonoBehaviour
{
    [SerializeField]
    private EndScreen _endScreen;

    void Start()
    {
        CustomerManager.AllServed += OnAllServed;
        _endScreen.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CustomerManager.AllServed -= OnAllServed;
    }

    private void OnAllServed()
    {
        _endScreen.gameObject.SetActive(true);
    }
}
