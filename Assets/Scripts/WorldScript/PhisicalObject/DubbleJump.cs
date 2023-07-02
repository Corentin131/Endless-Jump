using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubbleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();
            playerScript.isTouchFloor = true;
        }
    }
}
