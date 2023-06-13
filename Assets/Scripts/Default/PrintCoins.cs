using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintCoins : MonoBehaviour
{
    public TextMeshProUGUI coins;
    
    int currentValue;

    void Start()
    {
        StartCoroutine(CoinAdder());
        currentValue = Bank.coins;
    }

    void Update()
    {
        coins.text = currentValue.ToString();
    }

    IEnumerator CoinAdder()
    {
        while(true)
        {
            yield return null;
            if (Bank.coins > currentValue+200)
            {
                currentValue = Bank.coins;
            }else if(currentValue < Bank.coins)
            {
                while (currentValue < Bank.coins)
                {
                    LeanTween.scale(gameObject,new Vector3(1.1f,1.1f,1.1f),0.1f);
                    yield return new WaitForSeconds(0.02f);
                    currentValue += 1;
                }
                
            }else
            {
                LeanTween.scale(gameObject,new Vector3(1f,1f,1f),0.1f);
            }
            
        }
    }
}
