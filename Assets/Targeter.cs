using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    
    public string defaultTarget;
    public Transform target;
    public float smooth = 0.08f;
    public float minDistance = 5;
    public bool scaleOverDistance;

    Vector3 initialScale;
    Vector3 velocity = Vector3.zero;
    PlayerMovement playerScript;
    bool isFollowing;

    void Start()
    {
        if (defaultTarget != "")
        {
            target = GameObject.FindWithTag(defaultTarget).transform;
        }
        initialScale = transform.localScale;
       
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position,target.position);

        if (distance < minDistance | isFollowing | minDistance == 0)
        {
            isFollowing = true;
            
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position,targetPosition, ref velocity, smooth);

            if(scaleOverDistance)
            {
                float percentDistance = (5/distance+0.5f)*100;
                float percentScale = (distance/5)*initialScale.x;

                transform.localScale = new Vector3(percentScale,percentScale,percentScale);
            }

        }
    }
}
