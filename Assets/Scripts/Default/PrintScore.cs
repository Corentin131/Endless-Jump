using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI minScoreText;
    public TextMeshProUGUI maxScoreText;
    public Transform logoPosition;
    public Animator fillAnimator;
    public Slider mySlider;
    public GiftsData giftsData;
    public GiftMaker giftMaker = new GiftMaker();
    public ParticleSystem particlesChangeChallenge;

    float currentScore;
    int currentIndex = 0;
    float currentMaxScore;
    float currentMinScore;

    int scoreInt;

    GiftsData.Gift currentGiftStruct;
    AnimatorScript animatorScript = new AnimatorScript();
    GameObject currentLogo = null;

    void Start()
    {
        NextChallenge();
    }

    void Update()
    {
        scoreInt = Convert.ToInt32(Game.app.score);

        currentScore = Game.app.score-currentMinScore;

        mySlider.value = currentScore;
        score.text = (scoreInt).ToString();

        if (currentScore >= (currentMaxScore-currentMinScore)-3)
        {
            fillAnimator.SetBool("Flash",true);
        }else
        {
            fillAnimator.SetBool("Flash",false);
        }
        
        if(scoreInt == currentGiftStruct.targetScore)
        {
            NextChallenge();
        }
    }

    void NextChallenge()
    {
        if (giftsData.gift.Length-1 >= currentIndex)
        {
            if (currentIndex > 0)
            {
                //verify which gift will be executed
                SetGift();
            }
            
            
            currentGiftStruct = giftsData.gift[currentIndex];
            

            ChangeLogo();
            ChangeText();
            currentIndex += 1;
        }

    }

    void SetGift()
    {
        particlesChangeChallenge.Play();
        print(currentGiftStruct.giftType);

        switch (currentGiftStruct.giftType)
        {
            case GiftMaker.GiftType.Dollars500:
                giftMaker.EarnDollars(500,currentGiftStruct.notificationLogo);
                break;
            
            case GiftMaker.GiftType.X2:
                giftMaker.ModifyMultiplier(2);
                break;

            case GiftMaker.GiftType.X3:
                giftMaker.ModifyMultiplier(3);
                break;
        }
    }


    void ChangeLogo()
    {
        GameObject logo = currentGiftStruct.logo;

        Debug.Log(currentGiftStruct.targetScore);

        if (currentIndex != 0)
        {
            StartCoroutine(ChangeLogoAnimation(currentLogo));
        }else
        {
            currentLogo = Instantiate(logo,logoPosition.position,logoPosition.rotation,transform);
        }
        
       

    }


    void ChangeText()
    {
        if (currentIndex == 0)//Quand la fonction est appelé pour le première fois
        {
            currentMinScore = 0;
        }else
        {
            currentMinScore = currentMaxScore;
        }

        currentMaxScore =currentGiftStruct.targetScore;

        Vector3 scale = new Vector3(0.8f,0.8f,0.8f);
        float time = 0.2f;

        minScoreText.text = currentMinScore.ToString();
        maxScoreText.text = currentMaxScore.ToString();

        StartCoroutine(animatorScript.ScaleAction(maxScoreText.gameObject.transform,scale,time));
        mySlider.maxValue = currentMaxScore-Game.app.score;
    }
    

    IEnumerator ChangeLogoAnimation(GameObject logo)
    {
        // Get out Logo
        LeanTween.scale(logo,new Vector3(0.45f,0.45f,0.45f),1f).setEaseInOutElastic();
        yield return new WaitForSeconds(1f);
        LeanTween.scale(logo,new Vector3(0,0,0),0.2f);
        yield return new WaitForSeconds(0.1f);

        Destroy(currentLogo);
        
        currentLogo = Instantiate(currentGiftStruct.logo,logoPosition.position,logoPosition.rotation,transform);
        animatorScript.ScaleApparition(currentLogo.transform);
    }



}
