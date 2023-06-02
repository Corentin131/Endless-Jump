using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverGuard : MonoBehaviour
{
    public Transform smartFollow;
    public GameObject player;

    Player playerScript;

    // Update is called once per frame
    void Start()
    {
        playerScript =player.GetComponent<Player>();
    }
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, smartFollow.position);
        if (dist > 20)
        {
            playerScript.Die();

        }
    }

}