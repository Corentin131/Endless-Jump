using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCenter : MonoBehaviour
{
    public Levels[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        GenerationCenter.Initialize(obstacles);
    }

}
