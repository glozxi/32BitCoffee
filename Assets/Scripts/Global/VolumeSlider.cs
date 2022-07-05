using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer masterMixer;

    public AnimationCurve volumeCurve;

    public Slider BGMSlider;
    public Slider SFXSlider;

    public void start()
    {
        //set slider position
        BGMSlider.value = PlayerPrefs.HasKey("BGMVolume") ? PlayerPrefs.GetFloat("BGMVolume") : 1f;
        SFXSlider.value = PlayerPrefs.HasKey("FXVolume") ? PlayerPrefs.GetFloat("FXVolume") : 1f;
    }

    public void SetBGMVolume()
    {
        //masterMixer.SetFloat("BGMVolume", -80f + (80f * BGMSlider.value));
        masterMixer.SetFloat("BGMVolume", -80f + (80f * volumeCurve.Evaluate(BGMSlider.value)));
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.Save();
    }
    public void SetFXVolume()
    {
        masterMixer.SetFloat("FXVolume", -80f + (80f * volumeCurve.Evaluate(SFXSlider.value)));
        PlayerPrefs.SetFloat("FXVolume", SFXSlider.value);
        PlayerPrefs.Save();
    }
}
