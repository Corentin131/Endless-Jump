using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaseShop : MonoBehaviour
{
    [Header("Buy Skin Menu")]
    public GameObject buySkinMenu;
    public TextMeshProUGUI priceBuyText;
    public TextMeshProUGUI nameBuyText;
    public Image buyLogo;
    public Image priceBackground;
    public Button buyButton;

    [Header("Choose Skin Menu")]
    public GameObject chooseSkinMenu;
    public Image chooseLogo;
    public TextMeshProUGUI nameChooseText;
    public Button chooseButton;

    [HideInInspector] public Skin skin;
    [HideInInspector] public bool bought = false;
    [HideInInspector] public bool choose = false;

    void Start()
    {
        priceBuyText.text = skin.price.ToString();
        nameBuyText.text = skin.skinName.ToString();
        buyLogo.sprite = skin.logo;
        /*
        chooseLogo = chooseSkinMenu.transform.Find("Image").GetComponent<Image>();
        nameChooseText = chooseSkinMenu.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        chooseButton = chooseSkinMenu.transform.Find("ChangeButton").GetComponent<Button>();
        */
        nameChooseText.text = skin.skinName.ToString();
        chooseLogo.sprite = skin.logo;

        

    }

    void Update()
    {
        if (bought == true)
        {
            chooseSkinMenu.SetActive(true);
            buySkinMenu.SetActive(false);

        }else if (Game.instance.coins < skin.price)
        {
            priceBackground.color =  new Color(0.91f, 0.3f, 0.21f);
            buyButton.interactable = false;
        }
    }

    public void VerifyState()
    {   
        foreach (Skin skinToVerify in Possession.instance.skinsUnlocked)
        {
            print(skin.skinName);
            if(skinToVerify.skinName == skin.skinName)
            {
                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                print("Change");
                return;
            }
        }

        if (Game.instance.coins < skin.price)
        {
            priceBackground.color =  new Color(0.91f, 0.3f, 0.21f);
            buyButton.interactable = false;
        }
    }

    public void Buy()
    {
        if (skin != null)
        {
            Game.instance.coins -= skin.price;
            Possession.instance.UnlockSkin(skin);
            Possession.instance.ChangeSkin(skin);

            //VerifyState();
        }
    }
}
