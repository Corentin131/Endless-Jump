using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App
{
    //Notifier
    public Notifier notifier;
    //Score Part
    public  float score;

    //BankPart
    public int coins = 0;
    public int multiplier = 1;

    public void AddCoins(int coin)
    {
        coins  += coin*multiplier;
    }

    public void Reset()
    {
        score = 0;
        multiplier = 1;
    }
}
