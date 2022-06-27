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

    public Character CreateCharacter(string characterName)
    {
        Character newChar = new Character(characterName);
        characterDictionary.Add(characterName, onScreen.Count);
        onScreen.Add(newChar);
        return newChar;
    }


    public void CMPreLoadChar(string characterName)
    {
        Character character = CreateCharacter(characterName);
        character.enabled = false;
    }
    public void CMChar(string characterName, string body, string expr, Vector2 pos, bool enabled = true)
    {
        float speed = 1f;

        Character character = GetCharacter(characterName);
        character.TransitBoth(character.GetSprite(body), character.GetSprite(expr), speed, false);
        character.MoveTo(pos);
        character.enabled = enabled;
    }

    public void CMChar(string characterName, string body, string expr, string pos, bool enabled = true)
    {
        if (body == "HIDE")
        {
            CMEnableChar(characterName, false);
            return;
        }
        float speed = 1f;

        Character character = GetCharacter(characterName);
        character.TransitBoth(character.GetSprite(body), character.GetSprite(expr), speed, false);
        character.MoveTo(pos);
        character.enabled = enabled;
    }

    public void CMEnableChar(string charName, bool TF= true){
        GetCharacter(charName).enabled = TF;
    }

    public void CMHideAll()
    {
        for (int i = 0; i < onScreen.Count; i++)
        {
            onScreen[i].enabled = false;
        }
    }
}
