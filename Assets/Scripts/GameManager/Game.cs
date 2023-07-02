using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    
    //Possessions
    [HideInInspector]
    public Possession possessions = new Possession();

    //Notifier
    public Notifier notifier;
    
    //Score Part
    public  float score;

    //BankPart
    public int coins = 0;
    public int multiplier = 1;


    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of 'Game' in the scene");
        }

        instance = this;
    }
    
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
