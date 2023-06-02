using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointGestion : MonoBehaviour
{
    public Transform[] points;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPlace= new Vector2(0,0);
        foreach (Transform point in points)
        {
            point.localPosition = currentPlace;
            currentPlace.x = currentPlace.x+distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            MoveLeft();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)| Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
        if (points[points.Length - 1].localPosition.x != 0)
        {
            Move(distance);
        }
    }
    public void MoveRight()
    {
        if (points[0].localPosition.x != 0)
        {
            Move(-distance);
        }
    }

    void Move(float distanceSpace)
    {
        foreach (Transform point in points)
        {
            Vector2 pointPosition = point.localPosition;

            if (pointPosition.x == distanceSpace)
            {
                point.localPosition = new Vector2(0,0);
            }else
            {
                point.localPosition = new Vector2(pointPosition.x-distanceSpace,pointPosition.y);
            }
            
        }
    }
}
