using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPage : MonoBehaviour
{
    public Transform point;
    public float smoothTime = 0.1f; 
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(point.position.x, point.position.y, transform.position.z);
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
