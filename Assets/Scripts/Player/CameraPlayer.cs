using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CameraPlayer : MonoBehaviour
{   
    public Transform ghostPlayerFollow;

    PlayerMovement playerScript;
    RaycastHit2D cameraHit;

    void Start()
    {
        playerScript = gameObject.GetComponent<PlayerMovement>();
    }


    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(0,-5.0f)),Color.red);
        cameraHit = Physics2D.Raycast(transform.position,transform.TransformDirection(new Vector2(0,-1)), 5.0f,3);

        if (cameraHit) 
        {
            ghostPlayerFollow.transform.rotation = transform.rotation;

            Vector2 position = transform.InverseTransformPoint(new Vector2(cameraHit.point.x,cameraHit.point.y));

            int correctionX = 3;
            int correctionY = 11;

            if (playerScript.direction == 1)
            {
                position = new Vector2(position.x+correctionY,position.y+correctionX);
            }else
            {
                position = new Vector2(position.x-correctionY,position.y+correctionX);
            }

            ghostPlayerFollow.transform.position = transform.TransformPoint(position);
        }
    }

}
