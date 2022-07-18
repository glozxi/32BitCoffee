using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionUnity : MonoBehaviour
{
    public Animator anim;
    public float transitTime = 1f;

    public void LoadNextLevel(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        anim.SetTrigger("FadeTransition"); 
        yield return new WaitForSeconds(transitTime);
        SceneManager.LoadScene(scene);
    }
}
