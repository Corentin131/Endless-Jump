using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWall : MonoBehaviour
{
    public GameObject[] wallParts;
    public float forceOnDestroyX;
    public float forceOnDestroyY;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();

            if (playerScript.isBoosting == true)
            {
                DestroyWall();
            }else
            {
                print("die");
            }
        }
    }

    void DestroyWall()
    {
        foreach(GameObject parts in wallParts)
        {
            Rigidbody2D rbPart = parts.GetComponent<Rigidbody2D>();
            rbPart.bodyType = RigidbodyType2D.Dynamic;
            rbPart.velocity = new Vector2(forceOnDestroyX*Time.deltaTime,forceOnDestroyY*Time.deltaTime);
            rbPart.angularVelocity = 900;
        }
    }
}
