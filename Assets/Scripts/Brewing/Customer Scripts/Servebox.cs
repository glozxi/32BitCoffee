using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Servebox : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;

    public delegate void Collision(Cup cup);
    public event Collision CupCollision;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(EventSystem.current);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObject)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
                    if (Array.Exists(hits, hit => hit.collider.gameObject.GetComponent<Cup>() != null))
                    {
                        foreach (var hit in hits)
                        {
                            if (hit.collider.gameObject.GetComponent<Cup>() != null)
                            {
                                CupCollision?.Invoke(hit.collider.gameObject.GetComponent<Cup>());
                                return;
                            }
                        }
                    }
                    return;
                }

            }
        }
    }
}
