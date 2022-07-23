using UnityEngine;
using UnityEngine.UI;

public class TextSpeedSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    void Start()
    {
        _slider.value = PlayerPrefs.HasKey("TextSpeed") ? PlayerPrefs.GetFloat("TextSpeed") : 0.5f;
        SetTextSpeed();
    }

    public void SetTextSpeed()
    {
        float delay = (_slider.maxValue - _slider.value) / 10;
        InkManager inkManager = FindObjectOfType<InkManager>();
        if (inkManager != null) inkManager.TypewriterDelay = delay;
        PlayerPrefs.SetFloat("TextSpeed", _slider.value);
        PlayerPrefs.Save();
    }
}
