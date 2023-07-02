using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class SkinManager : MonoBehaviour
{
    Skin currentSkin;
    PlayerMovement playerMovement;
    
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        ChangeSkin(Possession.instance.currentSkin);
    }

    void Update()
    {
        Skin skin = Possession.instance.currentSkin;
       
        if (currentSkin.skinName != skin.skinName)
        {
            ChangeSkin(skin);
        }
    }

    void ChangeSkin(Skin skin)
    {
        print("Hello");
        GameObject image = Instantiate(skin.skinObject, transform.position , transform.rotation,transform);
        playerMovement.flyTrail = image.transform.Find("Fly");
        playerMovement.movingOnFloorTrail = image.transform.Find("OnFloor");
        playerMovement.image = image;
        currentSkin = skin;
    }
}
