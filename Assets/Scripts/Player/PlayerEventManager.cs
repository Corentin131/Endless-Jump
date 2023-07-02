using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PlayerMovement))]
public class PlayerEventManager : MonoBehaviour
{
    public Transform ghostFollow;
    public string sceneName;

    PlayerMovement playerMovement;
    AnimatorScript animatorScript = new AnimatorScript();

    private void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(ghostFollow.position, transform.position);
        if (dist > 20)
        {
            Die();

        }
    }

    public void Die()
    {
        
        GenerationCenter.Reset();
        playerMovement.ChangeGravity(0);
        Game.instance.Reset();
        SceneManager.LoadScene(sceneName);
        print("die");
    }
    
    public void ReceiveMoney()
    {
        if (animatorScript.isPlaying == false)
        {
            StartCoroutine(animatorScript.ScaleAction(playerMovement.image.transform,new Vector3(1.2f,1.4f,1.3f),0.1f,add:false,initialScale:new Vector3(1,1,1)));
        }
    }
}
