using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubbleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.isTouchFloor = true;
            print(playerScript.isTouchFloor);
        }
    }
}
