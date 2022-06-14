using UnityEngine;
using TMPro;

public class AnalyseOrder : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    [SerializeField]
    private GameObject _altOrder;

    private Ray ray;

    private bool _isAltOrderDisplayed = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _collider = GetComponent<Collider2D>();

        _altOrder.SetActive(false);
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (IsMouseHovering(ray))
        {
            OnHover();

            // 0 for primary button
            if (Input.GetMouseButtonDown(0))
            {
                if (_isAltOrderDisplayed)
                {
                    _altOrder.SetActive(false);
                }
                else
                {
                    _altOrder.SetActive(true);
                }
                _isAltOrderDisplayed = !_isAltOrderDisplayed;

            }
        }
        else
        {
            OnStopHover();
        }


    }

    private bool IsMouseHovering(Ray ray)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == _collider)
            {
                return true;

            }
        }
        return false;
        
    }


    private void OnHover()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnStopHover()
    {
        _spriteRenderer.enabled = false;
    }



}
