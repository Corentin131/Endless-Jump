using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public Transform[] targetObjects;
    public float moveSpeed = 5f;
    public int loop = 1;
    public bool canMove = true;
    private int currentIndex = 0;
    
    RaycastHit2D hit;
    int currentArrivals = 0;
    int currentLoop = 0;

    // Update is called once per frame
    void Update()
    {
       
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObjects[currentIndex].position, moveSpeed*Time.deltaTime);

            if (transform.position == targetObjects[currentIndex].position)
            {
                currentIndex = (currentIndex + 1) % targetObjects.Length;
                currentArrivals ++;
            }

            if (currentArrivals == targetObjects.Length)
            {
                currentArrivals = 0;
                currentLoop ++;
                print(loop);
            }

            if (loop <= currentLoop & loop != 0)
            {
                canMove = false;
            }
        }
    }

    public void StartMoving()
    {
        canMove = true;
    }
}
