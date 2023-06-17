using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gifts", menuName = "Gifts", order = 2)]
public class GiftsData : ScriptableObject
{
    [System.Serializable]
    public struct Gift
    {
        public GameObject logo;
        public Sprite notificationLogo;

        public GiftMaker.GiftType giftType;
        
        public int targetScore;
    }
    
    public Gift[] gift;
    
}

