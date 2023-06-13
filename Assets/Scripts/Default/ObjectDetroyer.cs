using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetroyer : MonoBehaviour
{
    public float delay = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GlobalFunctions.Destroy(2,gameObject));
    }
}
