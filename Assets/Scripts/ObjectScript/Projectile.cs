using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float destroyDelay;

    void Start()
    {
        StartCoroutine(DestroyGameObject());
    }
    void Update()
    {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
