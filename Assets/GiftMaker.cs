using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftMaker
{
    public enum GiftType 
    {
        Dollars500,
        X2,
        X3
    }
    public void EarnDollars(int value,Sprite logo)
    {
        Game.app.AddCoins(value);
        Game.app.notifier.Notify("You win "+value.ToString(),logo);
    }

    public void ModifyMultiplier(int value)
    {
        Game.app.multiplier = value;
    }
}
