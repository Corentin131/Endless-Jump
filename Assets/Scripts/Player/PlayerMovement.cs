using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20;
    public float jumpPower = 20;
    public Animator animator;
    public GameObject image;

    [HideInInspector] public int direction = 1;
    [HideInInspector] public bool isTouchFloor = true;
    [HideInInspector] public bool isBoosting = true;
    [HideInInspector] public bool needToJump = false;
    [HideInInspector] public float currentRunningSpeed;

    [HideInInspector] public bool isTransiting;
    [HideInInspector] public bool rotationEffect;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public RaycastHit2D finalHit;

    public CurrentState currentState = new CurrentState();
    TransitData transitData = new TransitData();
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 200;
        currentRunningSpeed = 0;
        rb = transform.GetComponent<Rigidbody2D>();
        StartCoroutine(GetInput());
    }
    
    void Update()
    {
        currentState.ExecuteState(this);

        float y = transform.InverseTransformDirection(rb.velocity).y;
        rb.velocity = transform.TransformDirection(new Vector2(0,y));;

        if (currentRunningSpeed > 0)
        {
            if (isTouchFloor)
            {
                currentState = new MovingOnFloorState();
            }
            else if(TransitionMaker())
            {
                GravityChanger gravityChanger = finalHit.transform.gameObject.GetComponent<GravityChanger>();

                if (currentState.GetType() != typeof(TransitState) & gravityChanger.transition)
                {
                    bool transit =  transitData.SetData(image.transform , finalHit);
                    
                    if (transit)
                    {
                        currentState = new TransitState(transitData,image.transform,finalHit);
                    }
                }
            }
            else if (currentState.GetType() != typeof(TransitState))
            {
                currentState = new FlyState();
            }

            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            currentState = new MovingOnFloorState();

            currentRunningSpeed = speed;

            transform.eulerAngles =  new Vector3(0,0,0);
        }
        
    }

    void FixedUpdate()
    {
        isTouchFloor = false;
        if (needToJump)
        {
           currentState.Jump(this);
           needToJump = false;
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            isTouchFloor = true;     
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


    public void StartBoost()
    {
        isBoosting = true;
        currentRunningSpeed = speed*2;
    }

    public void StopBoost()
    {
        isBoosting = false;
        currentRunningSpeed = speed;
    }

    bool TransitionMaker()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(4*direction,4f)),Color.red);
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
            finalHit = TransitionHitRight;
        }

        if (finalHit)
        {
            if(finalHit.transform.tag == "Platform")
            {
                if (currentState.GetType() != typeof(TransitState))
                {
                    return true;
                }
                
            }
            
        }else  if (currentState.GetType() == typeof(TransitState))
        {
            currentState = new CurrentState();
        }
        return false;
    }

    IEnumerator GetInput()
    {
        while(true)
        {
            yield return null;

                //float y = transform.InverseTransformDirection(rb.velocity).y;
                //rb.velocity = transform.TransformDirection(new Vector2(0,y));

                if (Input.GetMouseButtonDown(0) & currentRunningSpeed != 0)
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
}

public class CurrentState
{
    public virtual CurrentState ExecuteState(PlayerMovement playerMovement)
    {
        GameObject image = playerMovement.image;
        playerMovement.transform.Translate(new Vector2(playerMovement.currentRunningSpeed*Time.deltaTime*playerMovement.direction,0));

        if (playerMovement.direction == 1)
        {
            image.transform.localScale = new Vector3(Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
        }
        else if (playerMovement.direction == -1)
        {
            image.transform.localScale = new Vector3(-Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
        }

        if (Input.GetKey(KeyCode.LeftAlt) | Input.GetKey(KeyCode.RightShift))
        {
            playerMovement.StartBoost();
        }else
        {
            playerMovement.StopBoost();
        }

        return this;
    }
    
    public void Jump(PlayerMovement playerMovement)
    {
        Rigidbody2D rb = playerMovement.rb;
        Transform playerTransform = playerMovement.transform;

        float x = playerTransform.transform.InverseTransformDirection(rb.velocity).x;
        rb.velocity = playerTransform.TransformDirection(new Vector2(x,playerMovement.jumpPower*Time.deltaTime));

        playerMovement.currentState = new FlyState();
    }
}

class MovingOnFloorState : CurrentState
{
    public override CurrentState ExecuteState(PlayerMovement playerMovement)
    {
        GameObject image = playerMovement.image;
        int direction = playerMovement.direction;

       image.transform.localEulerAngles = new Vector3(0,0,0);

        //rotate image effect
        
        
        return base.ExecuteState(playerMovement);
    }
}

class FlyState : CurrentState
{
    public override CurrentState ExecuteState(PlayerMovement playerMovement)
    {
        GameObject image = playerMovement.image;
        Transform playerTransform = playerMovement.transform;

        int direction = playerMovement.direction;
   
        Vector2 velocity = playerTransform.InverseTransformDirection(playerMovement.rb.velocity);
        image.transform.eulerAngles = new Vector3(0,playerTransform.eulerAngles.y,playerTransform.eulerAngles.z+velocity.y*1.4f*direction);

        return base.ExecuteState(playerMovement);
    }
}

class TransitState : CurrentState
{
    public TransitData transitData;
    
    public Transform imageTransform;

    public RaycastHit2D raycast;
    public bool transiting;

    public TransitState(TransitData transitData,Transform imageTransform,RaycastHit2D raycast)
    {
        GravityChanger gravityChanger = raycast.transform.gameObject.GetComponent<GravityChanger>();
    
        this.transiting = gravityChanger.transition;
        this.transitData = transitData;
        this.imageTransform = imageTransform;
        this.raycast = raycast;
    }

    public override CurrentState ExecuteState(PlayerMovement playerMovement)
    {
        
        float distance = Vector3.Distance(playerMovement.transform.position, new Vector2(playerMovement.finalHit.point.x,playerMovement.finalHit.point.y))-0.5f;

        float percentDistance = (distance/transitData.initialDistance)*100;
        float percentRotation = percentDistance/100*transitData.toRotate;

        imageTransform.localEulerAngles = new Vector3(0,0,transitData.initialZRotation+percentRotation-transitData.toRotate);

        return base.ExecuteState(playerMovement);
    }
}

class TransitData
{
    public float toRotate;
    public float initialDistance;
    public float initialZRotation;

    public bool SetData(Transform imageTransform, RaycastHit2D hit)
    {
        GravityChanger gravityChanger = hit.transform.gameObject.GetComponent<GravityChanger>();

        float distance = Vector3.Distance(imageTransform.position, new Vector2(hit.point.x,hit.point.y))-0.5f;
        float targetRotation = hit.transform.eulerAngles.z;

        initialZRotation = imageTransform.localEulerAngles.z;

        toRotate = -Mathf.DeltaAngle(initialZRotation, targetRotation);

        initialDistance = distance;

        return gravityChanger.transition;
    }
    
}