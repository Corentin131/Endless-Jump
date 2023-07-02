using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript
{
    public bool isPlaying = false;
    public IEnumerator ScaleAction(Transform transform,Vector3 vectorAugmentation,float duration,bool add = true,Vector3 initialScale = new Vector3())
    {
        isPlaying = true;

        float x = vectorAugmentation.x;
        float y = vectorAugmentation.y;
        float z = vectorAugmentation.z;

        if(add == true)
        {
            float direction = (Math.Abs(x)/x);

            x = (Math.Abs(x) + vectorAugmentation.x) * direction;// will have 1 or -1
            y = transform.localScale.y + y;
            z = transform.localScale.z + z;
        }

        Vector3 scale = transform.localScale;
       
        if (initialScale.x != 0)
        {
            scale = initialScale;
        }

        LeanTween.scale(transform.gameObject,new Vector3(x,y,z),duration);
        yield return new WaitForSeconds(duration);
        LeanTween.scale(transform.gameObject,scale,duration);
        yield return new WaitForSeconds(duration);

        isPlaying = false;
        
    }
    /*
    public IEnumerator alphaAction(Transform transform,float alphaTarget,float initialAlpha,float duration)
    {
        
        LeanTween.alpha(transform.gameObject,alphaTarget,duration);
        yield return new WaitForSeconds(duration);
        LeanTween.alpha(transform.gameObject,initialAlpha,duration);
        
    }
    */

    public void ScaleApparition(Transform transform)
    {
        Vector3 initialScale = transform.localScale;

        transform.localScale = new Vector3(0,0,0);

        LeanTween.scale(transform.gameObject,initialScale,0.8f).setEaseOutBounce();
    }
}
