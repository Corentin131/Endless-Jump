using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    public int direction = 1;
    public bool isTrigger = false;
    public bool transition = true;
    public bool rotationEffect = true;
    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player" & isTrigger == false)
        {
            Change(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player" & isTrigger == true )
        {
            Change(other.gameObject);
        }
    }

    void Change(GameObject other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.direction = direction;

        //Change rotation of the object
        Vector3 currentRotation = other.transform.rotation.eulerAngles;
        currentRotation.z = transform.rotation.eulerAngles.z;
        other.transform.rotation = Quaternion.Euler(currentRotation);

    
        playerMovement.ChangeGravity(transform.rotation.eulerAngles.z);
        playerMovement.rotationEffect = rotationEffect;

        

    }
}
