using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    public GameObject shopCaseExample;
    public Transform casesParent;
    public Skin[] skinsData;
    public CaseShop[] displayedSkins;

    int currentCoin = 0;

    void Update()
    {
       VerifyState();
    }

    void VerifyState()
    {
        foreach (CaseShop displayedSkin in displayedSkins)
        {
            foreach (Skin skinsUnlocked in Possession.instance.skinsUnlocked)
            {
                if(skinsUnlocked.skinName == displayedSkin.skin.skinName)
                {
                    //When is already by
                    displayedSkin.bought = true;
                    break;
                }
            }

            if (Game.instance.coins < displayedSkin.skin.price)
            {
                displayedSkin.priceBackground.color =  new Color(0.91f, 0.3f, 0.21f);
                displayedSkin.buyButton.interactable = false;
            }

        }

    }

    void OnEnable()
    {
        foreach (Skin skinData in skinsData)
        {
            DisplayShopCase(skinData);
        }
    }

    void OnDisable()
    {
        foreach (CaseShop displayedSkin in displayedSkins)
        {
            Destroy(displayedSkin.gameObject);
        }
        displayedSkins = new CaseShop[]{};
    }

    void DisplayShopCase(Skin skin)
    {
        GameObject shopCaseGameObject = Instantiate(shopCaseExample,transform.position,transform.rotation,casesParent);

        CaseShop caseShop = shopCaseGameObject.GetComponent<CaseShop>();
        caseShop.skin = skin;

        displayedSkins = displayedSkins.Concat(new [] {caseShop}).ToArray();
    }
}
