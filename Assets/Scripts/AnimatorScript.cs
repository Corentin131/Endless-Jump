using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript
{
    public bool isPlaying = false;
    public IEnumerator ScaleAction(Transform transform,Vector3 vectorAugmentation,float duration)
    {
        isPlaying = true;

        float x = transform.localScale.x + vectorAugmentation.x;
        float y = transform.localScale.y + vectorAugmentation.y;
        float z = transform.localScale.z + vectorAugmentation.z;

        Vector3 scale = transform.localScale;

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
