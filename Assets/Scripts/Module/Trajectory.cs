using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory
{
    public LineRenderer lineRenderer;
    public Transform initialTransform;
    public Vector2 initialVelocity;
    float gravityScale = 1.0f;
    int numPoints = 30;

    float timeInterval = 0.1f;

    Vector2 gravity;
    Quaternion rotation;

    public Trajectory(Transform initialTransform, Vector2 velocity, float yLimit, int direction, LineRenderer lineRenderer = null)
    {
        this.initialVelocity = new Vector2(velocity.x*direction,velocity.y);
        this.lineRenderer = lineRenderer;
        this.initialTransform = initialTransform;

        rotation = initialTransform.rotation;
        gravity = new Vector2(0,-9.80f);
    }

    public void DrawTrajectory()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = numPoints;

            Vector3 currentPosition = initialTransform.position;
            Vector2 currentVelocity = initialVelocity;


            for (int i = 0; i < numPoints; i++)
            {
                lineRenderer.SetPosition(i, currentPosition);

                currentVelocity += gravity * timeInterval;
                currentPosition += rotation * currentVelocity * timeInterval;   
            }
        }
    }

    public RaycastHit2D GetHit()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = numPoints;
        }
        Vector3 currentPosition = initialTransform.position;
        Vector2 currentVelocity = initialVelocity;

        for (int i = 0; i < numPoints; i++)
        {
            currentVelocity += gravity * timeInterval;
            currentPosition += rotation * currentVelocity * timeInterval;

            // Effectue une ligne de collision entre les points actuel et précédent
            if (i > 0)
            {
                
                RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentVelocity.normalized, currentVelocity.magnitude * timeInterval, 3);
                //RaycastHit2D hit = Physics2D.Linecast(previousPosition, currentPosition, collisionMask);
                if (hit.collider != null)
                {
                    // Collision détectée, arrête la trajectoire
                    if (lineRenderer != null)
                    {
                        lineRenderer.positionCount = i + 1;
                    }
                    return hit;
                }
            }
        }

        return new RaycastHit2D();
    }

    
}
