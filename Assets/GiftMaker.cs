using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftMaker : MonoBehaviour
{
    public GameObject coinCanvas;
    public Transform CoinText;
    public void EarnDollars(int value)
    {
        Bank.AddCoins(value);
    }

  
}
