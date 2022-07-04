using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public float MusicVolume { get; set; }
    public float FxVolume { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        MusicVolume = GetKeyValue("MusicValue", 1f);
        FxVolume = GetKeyValue("FxVolume", 1f);

        PlayerPrefs.Save();
    }

    float GetKeyValue(string KeyName, float DefaultValue = 1f)
    {

        if (PlayerPrefs.HasKey(KeyName))
        {
            return PlayerPrefs.GetFloat(KeyName);
        } else
        {
            PlayerPrefs.SetFloat(KeyName, DefaultValue);
        }
        return DefaultValue;
    }

    int GetKeyValue(string KeyName, int DefaultValue = 1)
    {

        if (PlayerPrefs.HasKey(KeyName))
        {
            return PlayerPrefs.GetInt(KeyName);
        }
        else
        {
            PlayerPrefs.GetInt(KeyName, DefaultValue);
        }
        return DefaultValue;
    }

    string GetKeyValue(string KeyName, string DefaultValue = "")
    {

        if (PlayerPrefs.HasKey(KeyName))
        {
            return PlayerPrefs.GetString(KeyName);
        }
        else
        {
            PlayerPrefs.GetString(KeyName, DefaultValue);
        }
        return DefaultValue;
    }
}
