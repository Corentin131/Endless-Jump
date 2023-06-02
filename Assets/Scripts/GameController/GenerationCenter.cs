using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenerationCenter
{
    public static int nbOfObstacles = 0;
    public static int limitOfObstacles = 5;
    public static int currentLevelIndex;
    public static Levels currentLevel;
    public static ObstacleData[] currentsObstacles;
    public static ObstacleData[] currentsRareObstacle;
    public static ObstacleData[] allObstacles;
    public static Levels[] levels;

    public static void Initialize(Levels[] listLevel)
    {
        levels = listLevel;
        currentLevel = levels[0];
        currentLevelIndex = 0;
        currentsObstacles = currentLevel.obstacles;
        currentsRareObstacle = currentLevel.rareObstacle;
        allObstacles = new ObstacleData[]{};
        AddAllLevelObstacle(currentLevel);
    }

    public static void Reset()
    {
        nbOfObstacles = 0;
        limitOfObstacles = 5;
    }

    public static ObstacleData[] RemoveArray(ObstacleData[] inputArray, ObstacleData elementToRemove) 
    {
        var indexToRemove = Array.IndexOf(inputArray, elementToRemove);

        if (indexToRemove < 0)
        {
            return inputArray;
        }

        var tempArray = new ObstacleData[inputArray.Length - 1];

        for (int i = 0, j = 0; i < inputArray.Length; i++) 
        {
            if (i == indexToRemove) 
            {
                continue;
            }
            tempArray[j] = inputArray[i];
            j++;
        }

        return tempArray;                                
    }

    public static void AddAllLevelObstacle(Levels level)
    {
        foreach(ObstacleData obstacle in level.obstacles)
        {
            allObstacles = allObstacles.Concat(new [] {obstacle}).ToArray();
        }
    }
}
