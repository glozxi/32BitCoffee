using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //https://www.raywenderlich.com/532-audio-tutorial-for-unity-the-audio-mixer might be better idea

    public static AudioManager instance;
    public static BGM activeBGM = null; 

    public static List<BGM> allBGM = new List<BGM>(); //Not used by anyone else
    public float bgmTransitionSpeed = 1f;
    public bool bgmTransitionSmooth = true;

    public static string globalPathFX = "Sound/FX/";
    public static Dictionary<string, string>  dictEfx = new Dictionary<string, string>()
    {
        {"buzzer", globalPathFX+"lisc_checked_unknown/166030__vinrax__metal-item-drop" }
    };

    public static string globalPathBG = "Sound/BGM/";
    public static Dictionary<string, string> dictBgm = new Dictionary<string, string>()
    {
        {"menu", globalPathBG+"Orange_Highway - Fukagawa" },
        {"two", globalPathBG+"Sunday_breakfast -  yuhei komatsu" }
    };


    // note if creating master sound, should be a multiplier because may set pitch or volume diff for particular fx.
    // two fx might have different volume or pitch
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(string file, float volume = 1f, float pitch = 1f)
    {      
        PlaySFX(Resources.Load(dictEfx[file]) as AudioClip, volume, pitch);
    }

    // two fx might have different volume or pitch
    public void PlaySFX(AudioClip file, float volume = 1f, float pitch = 1f)
    {
        //Appears on prefab.
        AudioSource source = CreateNewSource(string.Format("SFX_[{0}]", file.name));
        source.clip = file;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        Destroy(source.gameObject, file.length); //auto destroy self after finish 
    }

    public void PlayBGM(string file, float maxVolume = 1f, float pitch = 1f, float startingVolume = 0f, bool playOnStart = true, bool loop = true)
    {
        PlayBGM(Resources.Load(dictBgm[file]) as AudioClip, maxVolume, pitch, startingVolume, playOnStart, loop);
    }

    public void PlayBGM(AudioClip file, float maxVolume = 1f, float pitch = 1f, float startingVolume = 0f, bool playOnStart = true, bool loop = true)
    {
        if (file != null) 
        {
            for (int i = 0; i < allBGM.Count; i++)
            {
                BGM n = allBGM[i];
                if (n.clip == file) { 
                    activeBGM = n; 
                    break; 
                }
                //if song is not active or not the song we want to play
            }
            if (activeBGM == null || activeBGM.clip != file)
            {
                //if (activeBGM != null) activeBGM.Stop();
                activeBGM = new BGM(file, maxVolume, pitch, startingVolume, playOnStart, loop);
            }
        }

        //stop all the music if playbgm(null);
        else
        {
            activeBGM = null;
        }

        StopAllCoroutines();
        StartCoroutine(VolumeLeveler());
    }

    IEnumerator VolumeLeveler()
    {
        while (TransitionBGM())
        {
            yield return new WaitForEndOfFrame();
        }
    }

    bool TransitionBGM()
    {
        bool anyValueChanged = false;
        float speed = bgmTransitionSpeed * Time.deltaTime;

        for (int i = allBGM.Count - 1; i>= 0; i--)
        {
            BGM bgm = allBGM[i];
            if (bgm == activeBGM)
            {
                if (bgm.volume < bgm.maxVolume)
                {
                    bgm.volume = bgmTransitionSmooth ? Mathf.Lerp(bgm.volume, bgm.maxVolume, speed) : Mathf.MoveTowards(bgm.volume, bgm.maxVolume, speed);
                    anyValueChanged = true;
                }
            } 
            else
            {
                if (bgm.volume > 0)
                {
                    bgm.volume = bgmTransitionSmooth ? Mathf.Lerp(bgm.volume, 0f, speed) : Mathf.MoveTowards(bgm.volume, 0f, speed);
                    anyValueChanged = true;
                }
                else
                {
                    allBGM.RemoveAt(i);
                    bgm.DestroyBGM();
                    continue;
                }
            }
        }

        return anyValueChanged;

    }
    public static AudioSource CreateNewSource(string audioInput)
    {
        AudioSource newSource = new GameObject(audioInput).AddComponent<AudioSource>();
        newSource.transform.SetParent(instance.transform);
        return newSource;
    }

    //Unlike FX which is fired and forget, BGM may require you to start stop
    public class BGM
    {
        public AudioSource source;
        public AudioClip clip { get { return source.clip;  } set { source.clip = value; } }
        public float maxVolume = 1f;

        
        public BGM(AudioClip file, float desiredMaxVolume, float pitch, float startingVolume, bool playOnStart, bool loop)
        {
            source = AudioManager.CreateNewSource(string.Format("BGM_[{0}]", file.name));
            source.clip = file;
            source.volume = startingVolume;
            source.pitch = pitch;
            maxVolume = desiredMaxVolume;
            source.pitch = pitch;
            source.loop = loop;

            AudioManager.allBGM.Add(this);

            if (playOnStart)
            {
                source.Play();
            }
                
        }

        public float volume {  get { return source.volume; } set { source.volume = value; } }
        public float pitch { get { return source.pitch; } set { source.volume = pitch; } }

        public void Play()
        {
            source.Play();
        }
        public void Stop()
        {
            source.Stop();
        }
        public void Pause()
        {
            source.Pause();
        }

        //named this way becausse default name is that
        public void UnPause()
        {
            source.UnPause();
        }

        public void DestroyBGM()
        {
            AudioManager.allBGM.Remove(this);
            DestroyImmediate(source.gameObject);
        }
    }
    

}
