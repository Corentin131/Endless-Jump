using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    public int direction = 1;
    public bool isTrigger = false;

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
        Player playerMovement = other.GetComponent<Player>();
            
        //Change rotation of the object
        Vector3 currentRotation = other.transform.rotation.eulerAngles;
        currentRotation.z = transform.rotation.eulerAngles.z;
        other.transform.rotation = Quaternion.Euler(currentRotation);

        Vector3 currentRotationImage = playerMovement.image.transform.rotation.eulerAngles;
        currentRotationImage.z = transform.rotation.eulerAngles.z;
        playerMovement.image.transform.rotation = Quaternion.Euler(currentRotationImage);
    
        playerMovement.ChangeGravity(transform.rotation.eulerAngles.z);
        playerMovement.direction = direction;
    }
}
