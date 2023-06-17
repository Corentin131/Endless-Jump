using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject[] portalDestinations;
    public int[] directions;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            TeleportTo(other.gameObject);
        }
    }

    void TeleportTo(GameObject player)
    {
        PlayerMovement playerScript = player.gameObject.GetComponent<PlayerMovement>();
        
        int index = Random.Range(0, portalDestinations.Length);
        GameObject portal = portalDestinations[index];
        int direction = directions[index];

        player.transform.position = portal.transform.position;
        player.transform.rotation = portal.transform.rotation;
        playerScript.direction = direction;
    }
}
