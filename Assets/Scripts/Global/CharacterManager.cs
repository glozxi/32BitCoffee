using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** GetCharacter mybe the item you are looking for....
 */

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    // For layering
    public RectTransform characterPanel;

    // List of all spawned characters on set
    public List<Character> onScreen = new List<Character>();

    // For ease of reference
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        instance = this;
    }

    public Character GetCharacter(string characterName)
    {
        int index = -1;
        if (characterDictionary.TryGetValue(characterName, out index))
        {
            return onScreen[index];
        }

        return CreateCharacter(characterName);
    }

    public Character CreateCharacter(string _name)
    {
        Character newChar = new Character(_name);
        characterDictionary.Add(name, onScreen.Count);
        onScreen.Add(newChar);
        return newChar;
    }
}
