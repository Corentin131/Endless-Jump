using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GlobalFunctions
{
    
    public static void SpawnEffect(GameObject[] effects,Transform transform,Vector2 position,Transform parent = null)
    {
        foreach (GameObject effect in effects)
        {
            if (position == Vector2.zero)
            {
                position = transform.position;
            }

            GameObject effectSpawned = Object.Instantiate(effect,position,transform.rotation);
            if (parent != null)
            {
                effectSpawned.transform.parent = parent;
            }
        }
    }

    public static void RandomSpawner(GameObject gameObject,int nbOfSpawn,float minX,float maxX,float minY,float maxY,Transform parent = null , Transform objectFrom = null)
    {
        foreach (int value in Enumerable.Range(0, nbOfSpawn))
        {

            GameObject effectSpawned = Object.Instantiate(gameObject,new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY)),objectFrom.rotation);

            if (parent != null)
            {
                effectSpawned.transform.parent = parent;
            }
        }
    }
    public static IEnumerator Destroy(float second,GameObject toDestroy)
    {
        yield return new WaitForSeconds(second);
        Object.Destroy(toDestroy);
    }

    public static List<GameObject> FindObjectsWithTag(this Transform parent, string tag)
    {
        List<GameObject> taggedGameObjects = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == tag)
            {
                taggedGameObjects.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                taggedGameObjects.AddRange(FindObjectsWithTag(child, tag));
            }
        }
        return taggedGameObjects;
    }

    public static void VFXSwitch(Transform parent,bool switchOn)
    {
        foreach (TrailRenderer element in parent.GetComponentsInChildren<TrailRenderer>())
        {
            if (switchOn == true)
            {
                element.emitting = true;
            }else
            {
                element.emitting = false;    
            }
        }

        foreach (ParticleSystem element in parent.GetComponentsInChildren<ParticleSystem>())
        {
            if (switchOn == true)
            {
                element.Play();
            }else
            {
                element.Stop();
            }
        }
    }
}
