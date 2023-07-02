using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class PlayerScore : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Start()
    {
        StartCoroutine(AddScore());
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    IEnumerator AddScore()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            if (playerMovement.currentRunningSpeed > 0)
            {
                Game.instance.score += 0.01f;
            }
        }
    }
}
