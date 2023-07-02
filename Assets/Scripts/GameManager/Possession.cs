using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Possession : MonoBehaviour
{

    [HideInInspector] public static Possession instance;
    public Skin[] defaultsSkins;
    public Skin[] skinsUnlocked = new Skin[]{};
    public Skin currentSkin;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of 'Game' in the scene");
        }

        instance = this;

        if(defaultsSkins.Length != 0)
        {
             foreach (Skin defaultSkin in defaultsSkins)
            {
                UnlockSkin(defaultSkin);
            }

            ChangeSkin(skinsUnlocked[0]);
        }
        
    }
    
    public void UnlockSkin(Skin skinToUnlock)
    {   
        skinsUnlocked = skinsUnlocked.Concat(new [] {skinToUnlock}).ToArray();
    }

    public void ChangeSkin(Skin skinToChange)
    {
        currentSkin = skinToChange;
    }
}
