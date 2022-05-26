using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public List<GameObject> contents = new List<GameObject>();

    private string drinkMade;
    private Customer customer;

    private string CUPCONTENTTAG = "cupContent";

    [SerializeField]
    private GameObject cupContent;

    private int MAX_CONTENT = 4;

    [SerializeField]
    private Points points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        add(cupContent);
        
    }


    // Clears contents and displayed cup contents
    void clear()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(CUPCONTENTTAG))
        {
            Destroy(obj);
        }
        contents.Clear();
    }

    void reward()
    {
        // customer.serve(contents, drinkMade);
        // drinkMade = "";
        points.UpdatePoints(1);
        clear();
    }

    // Adds an item to the cup and displays it
    void add(GameObject cupContent)
    {
        /*
        if (contents.Count + 1 > MAX_CONTENT)
        {
            Debug.Log("Cup is now overflowing.");
            return;
        }
        */

        contents.Add(cupContent);
        display_content(cupContent);
        if (contents.Count == MAX_CONTENT)
        {
            reward();
        }
    }

    // Shows what it inside
    void display_content(GameObject cupContent)
    {
        Instantiate(cupContent,
            new Vector3(
                cupContent.transform.position.x,
                cupContent.transform.position.y + cupContent.GetComponent<SpriteRenderer>().bounds.size.y * cupContent.transform.localScale.y * contents.Count,
                0),
            Quaternion.identity);
    }
}
