using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject obstacle;
    public float delay;
    public void DestroyObstacle()
    {
        StartCoroutine(DestroyGameObject());
    }
    public IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(delay);
        GenerationCenter.nbOfObstacles -= 1;
        Destroy(obstacle);
    }
}
