using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coin;
    public int nbOfCoins = 1;
    public float distance = 1;
    public int spawnAllByChance = 0;
    public int maxSpawnRandom = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (maxSpawnRandom != 0)
        {
            nbOfCoins = Random.Range(0, maxSpawnRandom);
        }

        if (spawnAllByChance != 0)
        {
            if (Random.Range(0, spawnAllByChance) != 1)
            {
                nbOfCoins = 0;
            }
        }

        Vector2 position = transform.InverseTransformPoint(new Vector2(transform.position.x,transform.position.y));
        foreach (int nb in Enumerable.Range(0,nbOfCoins))
        {
            GameObject coinInstantiated = Instantiate(coin,transform.TransformPoint(new Vector2(position.x+nb*distance,position.y)),transform.rotation);
            coinInstantiated.transform.parent = transform;
            
        }
    }

}
