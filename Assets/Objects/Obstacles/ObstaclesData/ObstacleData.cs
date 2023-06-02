using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Obstacle", menuName = "Obstacle/Obstacle", order = 1)]
public class ObstacleData : ScriptableObject
{
    public new string name;
    public GameObject obstacle;
    public List<string> banedObstacle = new List<string>{};
}

