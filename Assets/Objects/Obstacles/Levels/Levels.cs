using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Levels", menuName = "Obstacle/Level", order = 2)]
public class Levels : ScriptableObject
{
    public int level;
    public ObstacleData[] obstacles;
    public ObstacleData[] rareObstacle;
}
