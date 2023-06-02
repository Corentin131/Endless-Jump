using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.Die();
        }
    }
}
