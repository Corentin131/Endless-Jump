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
    public LineRenderer lineRenderer;
    public LineRenderer lineRenderer2;
    public Transform teleportVFX;
    public GameObject teleportPoint;
    public GameObject[] jumpParticle;
    [HideInInspector] public int direction = 1;
    [HideInInspector] public bool isTouchFloor = true;
    [HideInInspector] public bool isBoosting = false;
    [HideInInspector] public bool needToJump = false;
    [HideInInspector] public float currentRunningSpeed;

    [HideInInspector] public bool isTransiting;
    [HideInInspector] public bool rotationEffect;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public RaycastHit2D finalHit;
    [HideInInspector] public Trajectory trajectory;
    [HideInInspector] public Transform flyTrail;
    [HideInInspector] public Transform movingOnFloorTrail;

    public CurrentState currentState = new CurrentState();

    void Start()
    {
        trajectory = new Trajectory(transform,new Vector2(9,jumpPower/137.5f),-20,direction,lineRenderer);
        isBoosting = false;
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

        if (currentRunningSpeed > 0){
            if (isTouchFloor & currentState.GetType() != typeof(TeleportState))
            {
                currentState = new MovingOnFloorState(this);
            }
            else if(currentState.GetType() != typeof(TransitState) & currentState.GetType() != typeof(FlyState) & currentState.GetType() != typeof(TeleportState))
            {
                //VerifyTransitOrFlyState(new Vector2(currentRunningSpeed/2.857143f,jumpPower/137.5f));
                currentState = new FlyState(this);
            }

        }else if (Input.GetMouseButtonDown(0))
        {
            currentState = new MovingOnFloorState(this);

            currentRunningSpeed = speed;

            transform.eulerAngles =  new Vector3(0,0,0);
        }

        DisplayTeleportPoint();
        //DrawBase();
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            //VerifyTransitOrFlyState(); 
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
        image.transform.localScale = new Vector3(1.5f*direction,0.8f,1);
    }

    public void StopBoost()
    {
        isBoosting = false;
        currentRunningSpeed = speed;
        image.transform.localScale = new Vector3(1f*direction,1f,1);
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
            //currentState = new CurrentState();
            return false;
        }
        return false;
        
    }
    //VFX
    public void DisplayTeleportPoint()
    {
        if (currentState.GetType() != typeof(FlyState))
        {
            Vector2 velocity = new Vector2(currentRunningSpeed/2.857143f,jumpPower/137.5f);

            Trajectory trajectory = new Trajectory(transform,velocity,-20,direction);

            trajectory.DrawTrajectory();
            RaycastHit2D hitCurve = trajectory.GetHit();
            if (hitCurve)
            {

                GravityChanger gravityChanger = hitCurve.transform.gameObject.GetComponent<GravityChanger>();

                if (hitCurve.transform.eulerAngles.z != transform.eulerAngles.z & hitCurve.transform.tag == "Platform")
                {
                    
                    Debug.DrawLine(transform.position, hitCurve.point, Color.red);
                
                    if (gravityChanger.teleportation == true )
                    {
                        lineRenderer2.SetPosition(0,transform.position);
                        lineRenderer2.SetPosition(1,hitCurve.point);
                        RaycastHit2D streightHit = Physics2D.Linecast(transform.position,hitCurve.point,3);
                        //teleportPoint.transform.position = streightHit.point;

                        lineRenderer2.enabled = true;
                        return;
                    }
                }
            }
            
            lineRenderer2.enabled = false;
        }
        
    }

    void DrawBase(int distance = 5)
    {
        Vector3 up = transform.TransformDirection(Vector3.up);
        Vector3 dawn = transform.TransformDirection(Vector3.down);

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, up, distance, 3);
        RaycastHit2D hitDawn = Physics2D.Raycast(transform.position, dawn, distance, 3);

        RaycastHit2D finalHit = new RaycastHit2D();
        Color rayColor = Color.blue;

        if (hitUp || hitDawn)
        {
            if (!hitUp)
            {
                finalHit = hitDawn;
            }
            else if (!hitDawn)
            {
                finalHit = hitUp;
            }
            else if (hitUp.distance < hitDawn.distance)
            {
                finalHit = hitUp;
            }
            else
            {
                finalHit = hitDawn;
            }

            Debug.DrawLine(transform.position, finalHit.point, rayColor);

            // When Raycast Detected :

            teleportPoint.transform.position = finalHit.point;
            teleportPoint.transform.rotation = finalHit.transform.rotation;
            teleportPoint.SetActive(true);
            return;
        }

        teleportPoint.SetActive(false);
    }   
    public void VerifyJumpState(Vector2 velocity)
    {
        trajectory = new Trajectory(transform,velocity,-20,direction,lineRenderer);
        //trajectory.DrawTrajectory();
        RaycastHit2D hit = trajectory.GetHit();
        
        if (hit)
        {
            if (hit.transform.eulerAngles.z != transform.eulerAngles.z & hit.transform.tag == "Platform")
            {
                GravityChanger gravityChanger = hit.transform.gameObject.GetComponent<GravityChanger>();
                if (gravityChanger.transition == true)
                {
                    currentState = new TransitState(this,hit,gravityChanger);
                    return;
                }
                else if (gravityChanger.teleportation == true & currentState.GetType() != typeof(TransitState) & currentState.GetType() != typeof(FlyState))
                {
                    Debug.Log(gravityChanger.teleportation);
                    Vector2 direction = hit.transform.TransformDirection(new Vector2(0,0.5f));
                    Vector2 target = direction+new Vector2(hit.point.x,hit.point.y);
                    currentState = new TeleportState(target,this,hit);
                    return;
                }
            }
        }
        
        currentState = new FlyState(this);
    }

    public void Flip(int direction)
    {
       //image.transform.localScale = new Vector3(direction*Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
       image.transform.localScale = new Vector3(direction*Math.Abs(image.transform.localScale.x),image.transform.localScale.y,image.transform.localScale.z);
    }

    IEnumerator GetInput()
    {
        while(true)
        {
            yield return null;

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
    public virtual CurrentState ExecuteState(PlayerMovement playerMovement,bool move = true,bool canBoost = true)
    {
        GameObject image = playerMovement.image;

        if (move == true)
        {
            playerMovement.transform.Translate(new Vector2(playerMovement.currentRunningSpeed*Time.deltaTime*playerMovement.direction,0));
        }
        
        playerMovement.Flip(playerMovement.direction);

        if (canBoost == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerMovement.StartBoost();
                
            }
            else if (playerMovement.isBoosting == true)
            {
                playerMovement.StopBoost();
            }
        }

        return this;
    }
    
    public void Jump(PlayerMovement playerMovement)
    {
        Rigidbody2D rb = playerMovement.rb;
        Transform playerTransform = playerMovement.transform;

        float x = playerTransform.transform.InverseTransformDirection(rb.velocity).x;
        rb.velocity = playerTransform.TransformDirection(new Vector2(x,playerMovement.jumpPower*Time.deltaTime));


        //reverify if there is a transition
        GlobalFunctions.SpawnEffect(playerMovement.jumpParticle,playerTransform,new Vector2(playerTransform.position.x,playerTransform.position.y-0.5f));
        playerMovement.VerifyJumpState(new Vector2(playerMovement.currentRunningSpeed/2.857143f,playerMovement.jumpPower/137.5f));
        //playerMovement.VerifyTransitOrFlyState(new Vector2(7,playerMovement.jumpPower/137.5f));
    }
}

class MovingOnFloorState : CurrentState
{
    public MovingOnFloorState(PlayerMovement playerMovement)
    {
        GlobalFunctions.VFXSwitch(playerMovement.flyTrail,false);
        GlobalFunctions.VFXSwitch(playerMovement.movingOnFloorTrail,true);
    }
    public override CurrentState ExecuteState(PlayerMovement playerMovement,bool move = true,bool canBoost = true)
    {
        GameObject image = playerMovement.image;
        int direction = playerMovement.direction;

       image.transform.localEulerAngles = new Vector3(0,0,0);

        //rotate image effect
        //playerMovement.Flip(playerMovement.direction);
        
        return base.ExecuteState(playerMovement);
    }
}

class FlyState : CurrentState
{

    public FlyState(PlayerMovement playerMovement)
    {
        GlobalFunctions.VFXSwitch(playerMovement.flyTrail,true);
        GlobalFunctions.VFXSwitch(playerMovement.movingOnFloorTrail,false);
    }

    public override CurrentState ExecuteState(PlayerMovement playerMovement,bool move = true,bool canBoost = true)
    {
        GameObject image = playerMovement.image;
        Transform playerTransform = playerMovement.transform;

        int direction = playerMovement.direction;
   
        Vector2 velocity = playerTransform.InverseTransformDirection(playerMovement.rb.velocity);
        image.transform.eulerAngles = new Vector3(0,playerTransform.eulerAngles.y,playerTransform.eulerAngles.z+velocity.y*2.2f*direction);

        return base.ExecuteState(playerMovement);
    }
}

class TransitState : CurrentState
{   
    /*
    public TransitData transitData;
    
    public Transform imageTransform;

    public RaycastHit2D raycast;
    public bool transiting;
    */

    public RaycastHit2D hit;
    public float initialDistance;
    public float toRotate;
    public GravityChanger gravityChanger;

    Vector3 initialRotation;

    public TransitState(PlayerMovement playerMovement,RaycastHit2D hit,GravityChanger gravityChanger)
    {
        this.hit = hit;
        this.initialDistance = Vector3.Distance(playerMovement.transform.position, new Vector2(hit.point.x,hit.point.y))-0.5f;
        this.toRotate = -Mathf.DeltaAngle(playerMovement.transform.eulerAngles.z-90, hit.transform.eulerAngles.z);
        Debug.Log(this.toRotate);
        this.gravityChanger = gravityChanger;
        this.initialRotation = playerMovement.image.transform.localEulerAngles;
    }


    public override CurrentState ExecuteState(PlayerMovement playerMovement,bool move = true,bool canBoost = true)
    {
        float distance = Vector3.Distance(playerMovement.transform.position, new Vector2(hit.point.x,hit.point.y))-0.5f;

        float percentDistance = (distance/initialDistance)*100;
        float percentRotation = percentDistance/100*toRotate;

        Transform imageTransform = playerMovement.image.transform;

        imageTransform.localEulerAngles = new Vector3(0,0,(playerMovement.transform.localEulerAngles.z+percentRotation-toRotate));

        playerMovement.Flip(gravityChanger.direction);
        
        return base.ExecuteState(playerMovement);
    }
}

class TeleportState : CurrentState
{
    public Vector2 target;
    public RaycastHit2D raycastHit2D;
    public TeleportState(Vector2 target,PlayerMovement playerMovement,RaycastHit2D raycastHit2D)
    {
        this.target = target;
        this.raycastHit2D = raycastHit2D;
        target  = playerMovement.transform.InverseTransformPoint(target);
        float rotation = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        playerMovement.image.transform.eulerAngles = new Vector3(0, 0, rotation);

        GlobalFunctions.VFXSwitch(playerMovement.flyTrail,false);
        GlobalFunctions.VFXSwitch(playerMovement.movingOnFloorTrail,false);
        GlobalFunctions.VFXSwitch(playerMovement.teleportVFX , true);
    }

    public override CurrentState ExecuteState(PlayerMovement playerMovement,bool move = false,bool canBoost = false)
    {   
        Vector2 direction = playerMovement.image.transform.TransformDirection(new Vector2(60*Time.deltaTime,0));
        playerMovement.transform.Translate(direction);

        playerMovement.rb.bodyType = RigidbodyType2D.Static;

        float distance = Vector3.Distance(playerMovement.transform.position,target);

        if (distance < 2 || distance > 10)
        {
            playerMovement.transform.position = target;
            playerMovement.transform.rotation = raycastHit2D.transform.rotation;

            playerMovement.rb.bodyType = RigidbodyType2D.Dynamic;

            playerMovement.currentState = new MovingOnFloorState(playerMovement);

            GlobalFunctions.VFXSwitch(playerMovement.teleportVFX , false);
        }
        
        return base.ExecuteState(playerMovement,move = false,canBoost = false);
    }

}