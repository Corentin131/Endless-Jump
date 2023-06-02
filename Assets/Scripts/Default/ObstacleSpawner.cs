using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<string> bannedObstacle = new List<string>{};
    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return null;
            if(GenerationCenter.currentsObstacles.Length != 0)
            {
                if (GenerationCenter.nbOfObstacles < GenerationCenter.limitOfObstacles)
                {   
                    int decideRareObstacle = Random.Range(0, 4);

                    ObstacleData obstacleData = GenerationCenter.currentsObstacles[Random.Range(0, GenerationCenter.currentsObstacles.Length)];
                    if (!bannedObstacle.Contains(obstacleData.name))
                    {
                            GameObject obstacle = Instantiate(obstacleData.obstacle,gameObject.transform.position,transform.rotation);
                            ObstacleSpawner obstacleScript = obstacle.GetComponent<ObstacleSpawner>();
                            GenerationCenter.nbOfObstacles+=1;
                            GenerationCenter.currentsObstacles = GenerationCenter.RemoveArray(GenerationCenter.currentsObstacles,obstacleData);
                            Destroy(gameObject);
                            break;
                        
                    }
                }
            }else
            {
                if(GenerationCenter.levels[GenerationCenter.levels.Length-1] != GenerationCenter.currentLevel)//verify if it's the last level
                {
                    GenerationCenter.currentLevelIndex++;
                    GenerationCenter.currentLevel = GenerationCenter.levels[GenerationCenter.currentLevelIndex];
                    GenerationCenter.AddAllLevelObstacle(GenerationCenter.currentLevel);
                    StartCoroutine(AddToCurrentObstacle(GenerationCenter.currentLevel.obstacles.Length,false));

                }else
                {
                    StartCoroutine(AddToCurrentObstacle(GenerationCenter.limitOfObstacles,true));
                }

                
            }
        }

        IEnumerator AddToCurrentObstacle(int nbOfObstacle,bool isGateToOtherLevel)
        {
            foreach (int i in Enumerable.Range(0, nbOfObstacle))
            {
                while (true)
                {
                    ObstacleData obstacleToAdd = null;

                    int decideRareObstacle = Random.Range(0, 4);
                    if (decideRareObstacle == 0 & GenerationCenter.currentsRareObstacle.Length != 0)
                    {
                        obstacleToAdd = GenerationCenter.currentsRareObstacle[Random.Range(0,GenerationCenter.currentsRareObstacle.Length)];
                        
                    }else if (isGateToOtherLevel)
                    {
                        
                        obstacleToAdd = GenerationCenter.allObstacles[Random.Range(0, GenerationCenter.allObstacles.Length)];
                        
                    }else
                    {
                        obstacleToAdd = GenerationCenter.currentLevel.obstacles[Random.Range(0,  GenerationCenter.currentLevel.obstacles.Length)];
                    }

                    if(!GenerationCenter.currentsObstacles.Contains(obstacleToAdd))
                    {
                        if (!bannedObstacle.Contains(obstacleToAdd.name))
                        {
                            GenerationCenter.currentsObstacles = GenerationCenter.currentsObstacles.Concat(new [] {obstacleToAdd}).ToArray();
                            break;
                        }

                    }
                    yield return null;
                }
                yield return null;
            }
        }
        
    }


}
