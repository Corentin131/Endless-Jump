using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public bool toPlayer;

    public Transform target;
    public float distanceToDestroy;
    public GameObject[] effectsWithPlayer;
    public GameObject[] effectsWithNotPlayer;

    PlayerEventManager playerScript;
    bool isFollowing;

    void Start()
    {
        if (toPlayer)
        {
            target = GameObject.FindWithTag("Player").transform;
            playerScript = target.gameObject.GetComponent<PlayerEventManager>();
        }

    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position,target.position);
        {

            if (distance < distanceToDestroy)
            {
                if (toPlayer)
                {
                    playerScript.ReceiveMoney();
                }
                
                Game.app.AddCoins(value);

                GlobalFunctions.SpawnEffect(effectsWithPlayer,target,Vector3.zero,parent:target);
                GlobalFunctions.SpawnEffect(effectsWithNotPlayer,target,Vector3.zero);
                Destroy(gameObject);
            }
        }
    }
}
