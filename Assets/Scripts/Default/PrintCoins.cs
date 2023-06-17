using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintCoins : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI multiplierText;
    public ParticleSystem bigAddParticle;
    
    int currentMultiplier;
    int currentValue;
    AnimatorScript animatorScript = new AnimatorScript();
    void Start()
    {
        StartCoroutine(CoinAdder());
        currentValue = Game.app.coins;
    }

    void Update()
    {
        MultiplierManager();

        string coinText = Simplify(currentValue);
        coins.text = coinText;
    }

    IEnumerator CoinAdder()
    {
        while(true)
        {
            yield return null;
            if (Game.app.coins > currentValue+200)
            {
                currentValue = Game.app.coins;
                bigAddParticle.Play();

            }else if(currentValue < Game.app.coins)
            {
                while (currentValue < Game.app.coins)
                {
                    LeanTween.scale(gameObject,new Vector3(1.1f,1.1f,1.1f),0.1f);
                    yield return new WaitForSeconds(0.02f);
                    currentValue += 1;
                }
                LeanTween.scale(gameObject,new Vector3(1f,1f,1f),0.1f);
                
            }
            
        }
    }

    string Simplify(int value)
    {
        float floatValue = (float)value;
        string toReturn = "";

        if (floatValue <= 1000)
        {
            toReturn = floatValue.ToString();
        }
        else if(floatValue >= 1000 & floatValue <= 999_999)
        {
            toReturn = (floatValue/1000).ToString("0.0")+"K";
        }
        else if (floatValue <= 999_999_999)
        {
            toReturn = (floatValue/1_000_000).ToString("0.0")+"M";
        }
        else if (floatValue <= 999_999_999_999)
        {
            toReturn = (floatValue/1_000_000_000).ToString("0.0")+"MD";
        }

        return toReturn.Replace(",",".");
    }

    void MultiplierManager()
    {
        if (currentMultiplier != Game.app.multiplier)//On change
        {
            
            StartCoroutine(animatorScript.ScaleAction(multiplierText.transform,new Vector3(0.5f,0.5f,0.5f),0.2f));
            currentMultiplier = Game.app.multiplier;
        }

        if (currentMultiplier > 1)
        {
            multiplierText.text = "X"+currentMultiplier.ToString();
        }
        
    }

}
