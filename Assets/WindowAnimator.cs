using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnimator : MonoBehaviour
{
    public Vector2 to;
    public float time;
    public RectTransform border;

    public Action actionOnFinish;

    public enum Action
    {
        Destroy,
        Unable
    };

    void Start()
    {
        if (Math.Abs(to.x) == 1)
        {
            to.x = border.sizeDelta.x*to.x;
        }if (Math.Abs(to.y) == 1)
        {
            to.y = border.sizeDelta.y*to.y;
        }
    }

    public void ExitSlideY()
    {
        StartCoroutine(ExitSlideYNumerator());
    }

    IEnumerator ExitSlideYNumerator()
    {
        LeanTween.moveLocal(gameObject,to,time).setEaseInBack();

        yield return new WaitForSeconds(time);

        if (actionOnFinish == Action.Destroy)
        {
            Destroy(gameObject);
        }
        else if (actionOnFinish == Action.Unable)
        {
            gameObject.SetActive(false);
        }
    }
}
