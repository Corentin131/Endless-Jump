using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddScore());
    }

    IEnumerator AddScore()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.01f);

            Game.app.score += 0.01f;
                
            
        }
    }
}
