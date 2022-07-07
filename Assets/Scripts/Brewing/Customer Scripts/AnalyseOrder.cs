using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class AnalyseOrder : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;

    private Image _image;

    [SerializeField]
    private GameObject _altOrder;

    private bool _isAltOrderDisplayed = false;
    private bool _isAlreadyAnalysed = false;
    public bool IsAlreadyAnalysed
    {
        set => _isAlreadyAnalysed = value;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        EnableImage(false);

        _altOrder.SetActive(false);
    }

    private void Update()
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
                OnHover();
                EnableImage(true);
                return;
            }
            
        }
        EnableImage(false);

    }


    private void OnHover()
    {
        // 0 for primary button
        // Button clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (_isAltOrderDisplayed)
            {
                _altOrder.SetActive(false);
            }
            else
            {
                _altOrder.SetActive(true);

                if (!_isAlreadyAnalysed)
                {
                    Points points = FindObjectOfType<Points>();
                    points.AddAnalysePoints();
                    _isAlreadyAnalysed = true;
                }
            }
            _isAltOrderDisplayed = !_isAltOrderDisplayed;

        }
    }

    private void OnStopHover()
    {
        _image.enabled = false;
    }

    private void EnableImage(bool value)
    {
        Color tempColor = _image.color;
        tempColor.a = value ? 255f : 0f;
        _image.color = tempColor;
    }
}
