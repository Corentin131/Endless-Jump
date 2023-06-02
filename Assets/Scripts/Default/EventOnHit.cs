using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventOnHit : MonoBehaviour
{
    public UnityEvent events;
    RaycastHit2D hit;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(0,5.0f)),Color.red);
        hit = Physics2D.Raycast(transform.position,transform.TransformDirection(new Vector2(0,1)), 5.0f);
        if(hit)
        {
            if(hit.transform.tag == "Player")
            {
                events.Invoke();
            }
        }
    }
}
