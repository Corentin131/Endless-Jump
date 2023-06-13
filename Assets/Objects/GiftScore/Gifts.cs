using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gifts", menuName = "Gifts", order = 2)]
public class Gifts : ScriptableObject
{
    public GameObject[] logos;
    public string[] gifts;
    public int[] score;
}

