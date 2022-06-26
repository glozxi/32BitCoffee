using UnityEngine;

public class AnalyseOrder : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    [SerializeField]
    private GameObject _altOrder;

    private Ray _ray;

    private bool _isAltOrderDisplayed = false;
    private bool _isAlreadyAnalysed = false;
    public bool IsAlreadyAnalysed
    {
        set => _isAlreadyAnalysed = value;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _collider = GetComponent<Collider2D>();

        _altOrder.SetActive(false);
    }

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (IsMouseHovering(_ray))
        {
            OnHover();
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
                    Points.AddAnalysePoints();
                    _isAlreadyAnalysed = true;
                }
            }
            _isAltOrderDisplayed = !_isAltOrderDisplayed;

        }
    }

    private void OnStopHover()
    {
        _spriteRenderer.enabled = false;
    }



}
