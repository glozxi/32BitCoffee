using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTERAUDIO : MonoBehaviour
{
    public Vector2 position;
    public float volume, pitch;
    public AudioClip[] clips;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // CharacterManager.instance.CMChar("PERSON2", "person_pose", "happy", "MM");
            // CharacterManager.instance.CMChar("PERSON3", "person_default", "shocked", "MR");
            AudioManager.instance.PlaySFX("buzzer", 1, 1);
            AudioManager.instance.PlayBGM("menu", 1,1,1);
            BackgroundManager.instance.CMChangeBackground("day");
        }
    }
}
