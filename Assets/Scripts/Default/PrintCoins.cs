using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintCoins : MonoBehaviour
{
    public TextMeshProUGUI coins;
    void Update()
    {
        coins.text = Bank.coins.ToString();
    }
}
