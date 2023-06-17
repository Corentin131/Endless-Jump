using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static App app = new App();
    
    public Notifier notifier;

    private void Awake()
    {
        app.notifier = notifier;
    }
}
