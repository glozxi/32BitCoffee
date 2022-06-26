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
            CharacterManager.instance.CMChar("ADDER", "adder_default", "default", "MM");
            //BackgroundManager.instance.CMChangeBackground("day");
        }
    }
}
