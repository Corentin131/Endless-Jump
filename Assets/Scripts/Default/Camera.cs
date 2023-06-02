using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // le joueur à suivre
    public float smoothTime = 0.3f; // temps pour atteindre la position cible
    private Vector3 velocity = Vector3.zero; // vitesse courante

    void Update()
    {
        // Détermine la position cible en fonction de la position actuelle du joueur
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        
        // Utilise SmoothDamp pour se déplacer en douceur vers la position cible
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
