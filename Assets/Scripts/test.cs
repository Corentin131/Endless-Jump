using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master
{
    public virtual void Hello()
    {
        Debug.Log("caca");
    }
}

public class Esclave : Master
{
    public override void Hello()
    {
       
        Debug.Log("pipi");
    }
}
public class test : MonoBehaviour
{
    public Master esclave = new Esclave();

    private void Start()
    {
        esclave.Hello();
    }
}
