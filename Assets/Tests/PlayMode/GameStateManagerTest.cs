using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GameStateManagerTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator StartNewGameChangesSceneToDialogueScene()
    {
        SceneManager.LoadScene("MainMenu");
        yield return null;
        GameStateManager manager = GameObject.Find("NewGameButton").GetComponent<GameStateManager>();
        manager.StartNewGame();
        yield return null;
        Assert.AreEqual(SceneManager.GetActiveScene().name, "DialogueScene");
    }
}
