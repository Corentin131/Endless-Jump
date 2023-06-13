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
    public Gifts gifts;
    public GiftMaker giftMaker;
    public ParticleSystem particlesChangeChallenge;

    float currentScore;
    int currentIndex = 0;
    int scoreInt;
    float maxScore;
    float minScore;

    AnimatorScript animatorScript = new AnimatorScript();
    GameObject currentLogo = null;

    void Start()
    {
        NextChallenge(); 
    }
    void Update()
    {
        scoreInt = Convert.ToInt32(ScoreCenter.score);

        score.text = (scoreInt).ToString();

        currentScore = ScoreCenter.score-minScore;

        mySlider.value = currentScore;


        if (currentScore >= (maxScore-minScore)-3)
        {
            fillAnimator.SetBool("Flash",true);
        }else
        {
            fillAnimator.SetBool("Flash",false);
        }
        if(scoreInt == gifts.score[currentIndex])
        {
            NextChallenge();
        }
    }

    void NextChallenge()
    {
        if (gifts.score.Length-1 > currentIndex)
        {
            if (currentIndex > 0)
            {
                //execute gift
                particlesChangeChallenge.Play();
                if(gifts.gifts[currentIndex-1] == "500Dollars")
                {
                    giftMaker.EarnDollars(500);
                    
                }
        
            }
            if (currentIndex == 0)
            {
                currentLogo = Instantiate(gifts.logos[currentIndex],logoPosition.position,logoPosition.rotation,transform);

            }else if (gifts.logos[currentIndex] != null)
            {
                StartCoroutine(ChangeLogo(currentLogo));
            
                //currentLogo = Instantiate(gifts.logos[currentIndex],logoPosition.position,logoPosition.rotation,transform);
            }
            currentIndex += 1;
            minScore = gifts.score[currentIndex-1];
            maxScore = gifts.score[currentIndex];
            
            Vector3 scale = new Vector3(0.8f,0.8f,0.8f);
            float time = 0.2f;

            minScoreText.text = minScore.ToString();

            maxScoreText.text = maxScore.ToString();
            StartCoroutine(animatorScript.ScaleAction(maxScoreText.gameObject.transform,scale,time));
            mySlider.maxValue = maxScore-ScoreCenter.score;
        }
    }

    IEnumerator ChangeLogo(GameObject logo)
    {
        // Get out Logo
        LeanTween.scale(logo,new Vector3(0.45f,0.45f,0.45f),1f).setEaseInOutElastic();;
        yield return new WaitForSeconds(1f);
        LeanTween.scale(logo,new Vector3(0,0,0),0.2f);
        yield return new WaitForSeconds(0.1f);
        Destroy(currentLogo);

        // change logo
        currentLogo = Instantiate(gifts.logos[currentIndex-1],logoPosition.position,logoPosition.rotation,transform);
        
        Vector3 initialScale = currentLogo.transform.localScale;

        currentLogo.transform.localScale = new Vector3(0,0,0);

        LeanTween.scale(currentLogo,initialScale,0.8f).setEaseOutBounce();

    }
    
}
