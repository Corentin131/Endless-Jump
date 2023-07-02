using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
public int score;
   void OnTriggerEnter2D(Collider2D other)
   {
        Game.instance.score += score;
        Destroy(gameObject);
   }
}
