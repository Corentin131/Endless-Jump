using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoTextModifier : MonoBehaviour
{
    [SerializeField] private RectTransform backGround;
    [SerializeField] private RectTransform logo;
    [SerializeField] private int backGroundBorder;
    [SerializeField] private int logoBorder;

    [SerializeField] private LogoSide logoSide;
    
    private enum LogoSide
    {
        Left = -1,
        Right = 1
    }


    RectTransform text;
    
    void  Start()
    {
        text = gameObject.GetComponent<RectTransform>();
    }
    void Update()
    {
        backGround.sizeDelta = new Vector2(text.sizeDelta.x+backGroundBorder,backGround.sizeDelta.y);
        
        logo.localPosition = new Vector2(text.localPosition.x + (text.sizeDelta.x/2+logoBorder)*(int)logoSide,logo.localPosition.y);
        
    }
}
