using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
/*
public class Player : MonoBehaviour
{

    public float speed = 20;
    public float jumpPower = 20;
    public Animator animator;
    public GameObject image;
    public Camera mainCamera;

    [HideInInspector]
    public int direction = 1;
    [HideInInspector]
    public bool isTouchFloor = true;
    [HideInInspector]
    public bool isBoosting = true;
    [HideInInspector]
    public bool needToJump = false;
    [HideInInspector]
    public float currentRotationZ = 0;
    public GameObject CameraTarget;

    public GameObject[] effectsOnTouchFloor;
    
    public string sceneName;
    float runningSpeed;
    public bool isTransiting;
    public bool rotationEffect;
    RaycastHit2D CameraHit;
    
    RaycastHit2D finalHit;
    Rigidbody2D rb;

    Transform side;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 200;
        runningSpeed = 0;
        rb = transform.GetComponent<Rigidbody2D>();
        StartCoroutine(PlaceCamera());
        StartCoroutine(GetInput());
        StartCoroutine(Transition());
    }
    
    void Update()
    {
        if (direction == 1)
        {
            image.transform.localScale = new Vector3(Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
        }
        else if (direction == -1)
        {
            image.transform.localScale = new Vector3(-Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
        }

        if (runningSpeed != 0)
        {
            transform.Translate(new Vector2(runningSpeed*Time.deltaTime*direction,0));

            //rotate image effect

            if (!isTouchFloor & rotationEffect & !isTransiting)
            {
                Vector2 velocity = transform.InverseTransformDirection(rb.velocity);
                image.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z+velocity.y*1.4f*direction);
            }else
            {
                image.transform.localEulerAngles = new Vector3(0,image.transform.eulerAngles.y,0);
            }
           
            if (Input.GetKey(KeyCode.LeftAlt) | Input.GetKey(KeyCode.RightShift))
            {
                StartBoost();
            }else
            {
                StopBoost();
            }

        }else if (Input.GetMouseButtonDown(0))
        {
            runningSpeed = speed;
            StartCoroutine(AddScore());
            transform.eulerAngles =  new Vector3(0,0,0);
        }
        
    }

    void FixedUpdate()
    {
        isTouchFloor = false;
        if (needToJump)
        {
           Jump();
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            isTouchFloor = true;    
            TouchFloor();        
        }
    }

   

    
    public void ChangeGravity(float direction)
    {
        direction = direction-90;

        float angleInRadians = direction * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);
        
        Vector2 vector2D = new Vector2(x*9.8f, y*9.8f);

        Physics2D.gravity = vector2D;
    }

    public void Die()
    {
        GenerationCenter.Reset();
        ChangeGravity(0);
        GlobalData.Reset();
        SceneManager.LoadScene(sceneName);
        print("die");
    }

    void Jump()
    {
        float x = transform.InverseTransformDirection(rb.velocity).x;
        rb.velocity = transform.TransformDirection(new Vector2(x,jumpPower*Time.deltaTime));
        isTouchFloor = false;
        needToJump = false;
    }

    void StartBoost()
    {
        isBoosting = true;
        runningSpeed = speed*2;
    }

    void StopBoost()
    {
        isBoosting = false;
        runningSpeed = speed;
    }

    public void ReceiveMoney()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ReceiveCoin"))
        {
            animator.SetTrigger("ReceiveCoin");
        }
        
    }
    
    public void TouchFloor()
    {
        //GlobalFunctions.SpawnEffect(effectsOnTouchFloor,CameraHit.transform,new Vector2(CameraHit.point.x,CameraHit.point.y));
    }
    IEnumerator PlaceCamera()
    {
        while (true)
        {
            yield return null;

            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(0,-5.0f)),Color.red);
            CameraHit = Physics2D.Raycast(transform.position,transform.TransformDirection(new Vector2(0,-1)), 5.0f,3);
            if (CameraHit) 
            {
                CameraTarget.transform.rotation = transform.rotation;
                Vector2 position = transform.InverseTransformPoint(new Vector2(CameraHit.point.x,CameraHit.point.y));
                int correctionX = 3;
                int correctionY = 11;

                if (direction == 1)
                {
                    position = new Vector2(position.x+correctionY,position.y+correctionX);
                }else
                {
                    position = new Vector2(position.x-correctionY,position.y+correctionX);
                }
                //Augmenter le target camera un peu plus heut pour pas que la camera soit centré
                CameraTarget.transform.position = transform.TransformPoint(position);
            }
            
        }
    }

    IEnumerator Transition()
    {
        float toRotate = 0;
        float initialDistance = 0; 
        float initialZRotation = 0;
        
        while (true)
        {
            yield return null;
            
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(4f*direction,4f)),Color.red);
            RaycastHit2D TransitionHitTop = Physics2D.Raycast(transform.position,transform.TransformDirection(new Vector2(1*direction,1)), 4f,3);

            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(4f*direction,0)),Color.red);
            RaycastHit2D TransitionHitRight = Physics2D.Raycast(transform.position,transform.TransformDirection(new Vector2(1*direction,0)), 4,3);
             
            
            if (TransitionHitTop & !isTouchFloor)
            {
                finalHit = TransitionHitTop;

            }else if (TransitionHitRight & !isTouchFloor)
            {
                finalHit = TransitionHitRight;

            }else
            {
                toRotate = 0;
                initialDistance = 0; 
                initialZRotation = 0;
                finalHit = TransitionHitRight;
            }

            if (finalHit)
            {
                if(finalHit.transform.tag == "Platform")
                {
                    GravityChanger gravityChanger = finalHit.transform.gameObject.GetComponent<GravityChanger>();
                    isTransiting = true;
                    if(gravityChanger.transition)
                    {
                        float distance = Vector3.Distance(transform.position, new Vector2(finalHit.point.x,finalHit.point.y))-0.5f;
                        float targetRotation = finalHit.transform.eulerAngles.z;

                        if (toRotate == 0)
                        {
                            initialZRotation = image.transform.eulerAngles.z;

                            toRotate = -Mathf.DeltaAngle(initialZRotation, targetRotation);

                            initialDistance = distance;
                        }
                        
                        float percentDistance = (distance/initialDistance)*100;
                        float percentRotation = percentDistance/100*toRotate;
            
                        image.transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z+percentRotation-toRotate);
                    
                    }else
                    {
                        //image.transform.localEulerAngles = new Vector3(0,image.transform.eulerAngles.y,0);
                        isTransiting = false;
                    }
                }else
                {
                    isTransiting = false;
                }

                
            }else
            {
                isTransiting = false;
            }
        }
    }
    IEnumerator GetInput()
    {
        while(true)
        {
            yield return null;

                float y = transform.InverseTransformDirection(rb.velocity).y;
                rb.velocity = transform.TransformDirection(new Vector2(0,y));
                if (Input.GetMouseButtonDown(0) & runningSpeed != 0)
                {
                    if (isTouchFloor)
                    {
                        needToJump = true;
                    }else
                    {
                        print("to late");
                    }
                }
                
            
        }
    }

    IEnumerator AddScore()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.01f);

            GlobalData.score += 0.01f;
                
            
        }
    }
}
*/