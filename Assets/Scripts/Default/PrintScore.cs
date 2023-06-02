using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintScore : MonoBehaviour
{
    public TextMeshProUGUI score;
   
    void Update()
    {
        score.text = ScoreCenter.score.ToString();
    }
}
