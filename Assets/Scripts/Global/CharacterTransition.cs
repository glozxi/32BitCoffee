using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// To rename, not appropriate name. 
public class CharacterTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool TransitionImages(ref Image active, ref List<Image> allImg, float speed, bool smooth)
    {
        bool hasChange = false;
        smooth = false;

        //Smoothstep https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
        //speed *= Time.deltaTime / 1f;
        //speed *= speed * (3f - 2f * speed);

        //Some kind of lerp
        speed *= Time.deltaTime;
        for (int i = allImg.Count - 1; i >= 0; i--)
        {
            // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#lerp_stutter_fix
            // See above tfor smooth lerp!! See explaination: https://answers.unity.com/questions/885210/vector3lerp-slow-first-and-become-faster.html
            Image image = allImg[i];
            if (image == active && image.color.a < 1f)
            {
                //image.color = SetAlpha(image.color, smooth ? Mathf.SmoothDamp(image.color.a, 1f, ref speed, 0f) : Mathf.MoveTowards(image.color.a, 1f, speed));
                image.color = SetAlpha(image.color, smooth ? Mathf.Lerp(image.color.a, 1f, speed) : Mathf.MoveTowards(image.color.a, 1f, speed));
                hasChange = true;
            }
            else
            {
                //if alpha is 0, means transparent, faded out, remove from world
                if (image.color.a > 0)
                {
                    //Again figure oute LERP
                    image.color = SetAlpha(image.color, smooth ? Mathf.Lerp(image.color.a, 0f, speed) : Mathf.MoveTowards(image.color.a, 0f, speed));
                    hasChange = true;
                }
                else
                {
                    allImg.RemoveAt(i);
                    DestroyImmediate(image.gameObject);
                    continue;
                }
            }

            //if activeimage, fade in
        }

        return hasChange;
    }
    public static Color SetAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    // Copy above and do same for BG

}
