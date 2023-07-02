using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float destroyDelayProjectile;
    public float speedProjectile;
    public float intervals;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(intervals));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn(float intervals)
    {
        while (true)
        {
            GameObject projectileInstantiated = Instantiate(projectile,transform.position,transform.rotation);
            Projectile projectileScript =  projectileInstantiated.GetComponent<Projectile>();
            
            projectileScript.destroyDelay = destroyDelayProjectile;
            projectileScript.speed = speedProjectile;
            yield return new WaitForSeconds(intervals);
        }
    }
}
