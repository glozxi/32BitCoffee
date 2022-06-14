using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
    // TO IMPLEMENT: RAndom Character Generation
{  
    [HideInInspector] 
    public RectTransform root; //root of characer img.
    public string charaName;
    public string globalPath = "Images/Characters/";
    
    /* Positional related variables/stuff
     */
    //Position
    Vector2 targetPos;
    Coroutine moving;

    // To add specific location (anchor percentage)
    public Dictionary<string, Vector2> fixedPos = new Dictionary<string, Vector2>()
    {
        {"LL", new Vector2(0.1f,0f) },
        {"ML", new Vector2(0.25f,0f) },
        {"MM", new Vector2(0.5f,0f) },
        {"MR", new Vector2(0.75f,0f) },
        {"RR", new Vector2(0.9f,0f) }
    };

    public bool enabled
    {
        get { return root.gameObject.activeInHierarchy; }
        set { root.gameObject.SetActive(value); }
    }

    /** Positioning related Functions */
    bool isMoving { get { return moving != null; } }

    /** Affects where the image is positioned. */
    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    /** For displaying character */
    Renderers renderers = new Renderers();

    /** Force set position, not a gradual movement. Insta-tele */
    public void SetPosition(Vector2 target)
    {
        Vector2 padding = anchorPadding;
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPos.x, maxY * targetPos.y);
        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;
    }

    public void MoveTo(string Target)
    {
        MoveTo(fixedPos[Target]);
    }

    public void MoveTo(Vector2 Target)
    {
        stopMoving();
        moving = CharacterManager.instance.StartCoroutine(Moving(Target));
    }

    private void stopMoving()
    {
        if (isMoving) { CharacterManager.instance.StopCoroutine(moving); }
        moving = null;
    }

    IEnumerator Moving(Vector2 target)
    {
        targetPos = target;
        Vector2 padding = anchorPadding;
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        //Move min because Max will follow
        Vector2 minAnchorTarget = new Vector2(maxX * targetPos.x, maxY * targetPos.y);
        while (root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = Vector2.MoveTowards(root.anchorMin, minAnchorTarget, 1f);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }
        stopMoving();
    }
    

    /** Character class is where we get the items needed
     */
    public Character(string name)
    {
        charaName = name;

        // Get canvas image prefab containing sprite
        // IMPORTANT TO ADJUST PREFAB LOCATION <-----------------------------
        string temporaryFormat = globalPath + name + "/" + name;
        GameObject prefab = Resources.Load(temporaryFormat) as GameObject;

        CharacterManager cm = CharacterManager.instance;
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);
        //ob.transform.SetParent(cm.transform);

        // Save renderer
        root = ob.GetComponent<RectTransform>();
        renderers.bodyRenderer = ob.transform.Find("BodyLayer").GetComponentInChildren<Image>();
        renderers.expresionRenderer = ob.transform.Find("ExpressionLayer").GetComponentInChildren<Image>();

        // Add to Master List
        renderers.allBodyRenderer.Add(renderers.bodyRenderer);
        renderers.allExpresionRenderer.Add(renderers.expresionRenderer);
    }

    /** For renders */
    class Renderers
    {
        public Image bodyRenderer;
        public Image expresionRenderer;

        public List<Image> allBodyRenderer = new List<Image>();
        public List<Image> allExpresionRenderer = new List<Image>();
    }


    /** Retrieving Sprites */

    //Removed in preference of not using sprite sheet
    /**
    public void SetExpresion(int index)
    {
        renderers.expresionRenderer.sprite = GetSprite(index);
    }*/

    public Sprite GetSprite(string retrieve)
    {
        string temporaryFormat = globalPath + charaName + "/";
        Sprite sprite = Resources.Load<Sprite>(temporaryFormat + retrieve); 
        return sprite;

        /**
        // For spite sheets
        //Sprite location needs to be the same as for body (or at most one directory different)
        
        Sprite[] sprites = Resources.LoadAll<Sprite>(path + charaName);

        return sprites[index];
        */
    }

    // SetBody(GetSprite());
    /**
    public void SetSprite(string body, string expr)
    {
        SetBody(body);
        SetExpresion(expr);
    }
    public void SetBody(string retrieve)
    {
        renderers.bodyRenderer.sprite = GetSprite(retrieve);
    }
    // No body swapping, only expresion rn.
    public void SetExpresion(string retrieve)
    {
        renderers.expresionRenderer.sprite = GetSprite(retrieve);
    }
    **/
    
    //body trans
    Coroutine transitioningBody = null;
    bool isTransitBody { get { return transitioningBody != null; } }

    public void TransitBoth(Sprite body, Sprite expr, float speed, bool smooth)
    {
        TransitionBody(body, speed, smooth);
        TransitionExpr(expr, speed, smooth);
    }

    public void TransitionBody(Sprite sprite, float speed, bool smooth)
    {
        if (renderers.bodyRenderer.sprite == sprite) return;
        StopTransitionBody();
        transitioningBody = CharacterManager.instance.StartCoroutine(TransitioningBody(sprite, speed, smooth));
    }
    void StopTransitionBody()
    {
        if (isTransitBody) { CharacterManager.instance.StopCoroutine(transitioningBody); }
        transitioningBody = null;
    }

    public IEnumerator TransitioningBody(Sprite sprite, float speed, bool smooth)
    {
        for (int i = 0; i < renderers.allBodyRenderer.Count; i++)
        {
            Image image = renderers.allBodyRenderer[i]; // is current image
            if (image.sprite == sprite)
            {
                renderers.bodyRenderer = image; //set active
                break;
            }
        }

        if (renderers.bodyRenderer.sprite != sprite) // not active  yet
        {
            Image image = GameObject.Instantiate(renderers.bodyRenderer.gameObject, renderers.bodyRenderer.transform.parent).GetComponent<Image>();
            renderers.bodyRenderer = image;
            renderers.allBodyRenderer.Add(image);
            image.color = CharacterTransition.SetAlpha(image.color, 0f); //spawn invisible
            image.sprite = sprite;
        }
        while (CharacterTransition.TransitionImages(ref renderers.bodyRenderer, ref renderers.allBodyRenderer, speed, smooth))
        {
            yield return new WaitForEndOfFrame();
        }
        StopTransitionBody();
    }
    //end of body trans

    Coroutine transitioningExpr = null;
    bool isTransitExpr { get { return transitioningExpr != null; } }

    public void TransitionExpr(Sprite sprite, float speed, bool smooth)
    {
        if (renderers.expresionRenderer.sprite == sprite) return;
        StopTransitionExpr();
        transitioningExpr = CharacterManager.instance.StartCoroutine(TransitioningExpr(sprite, speed, smooth));
    }
    void StopTransitionExpr()
    {
        if (isTransitExpr) { CharacterManager.instance.StopCoroutine(transitioningExpr); }
        transitioningExpr = null;
    }

    public IEnumerator TransitioningExpr(Sprite sprite, float speed, bool smooth)
    {
        for (int i = 0; i < renderers.allExpresionRenderer.Count; i++)
        {
            Image image = renderers.allExpresionRenderer[i]; // is current image
            if (image.sprite == sprite)
            {
                renderers.expresionRenderer = image; //set active
                break;
            }
        }

        if (renderers.expresionRenderer.sprite != sprite) // not active  yet
        {
            Image image = GameObject.Instantiate(renderers.expresionRenderer.gameObject, renderers.expresionRenderer.transform.parent).GetComponent<Image>();
            renderers.expresionRenderer = image;
            renderers.allExpresionRenderer.Add(image);
            image.color = CharacterTransition.SetAlpha(image.color, 0f); //spawn invisible
            image.sprite = sprite;
        }
        while (CharacterTransition.TransitionImages(ref renderers.expresionRenderer, ref renderers.allExpresionRenderer, speed, smooth))
        {
            yield return new WaitForEndOfFrame();
        }
        StopTransitionExpr();
    }
}
