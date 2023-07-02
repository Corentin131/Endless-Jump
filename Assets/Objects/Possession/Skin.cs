using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Possession/Skin", order = 2)]

public class Skin : ScriptableObject
{
    public string skinName = "default";
    public Sprite logo;
    public GameObject skinObject;
    public int price;
}
