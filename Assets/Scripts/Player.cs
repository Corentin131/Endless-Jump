using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 20;
    public float jumpPower = 20;
    public Animation playerAnimation;
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
    public Rigidbody2D rb;
    public GameObject bottom;
    public GameObject CameraTarget;
    
    public string sceneName;
    float runningSpeed;
    bool isJumping;
    
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        runningSpeed = 0;
        rb = transform.GetComponent<Rigidbody2D>();
        StartCoroutine(PlaceCamera());
        StartCoroutine(GetTouch());
    }
    
    void Update()
    {
        //Instantiate(movement,transform.position,transform.rotation);
        Vector3 diff = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(3.8f, 3.2f) * Mathf.Rad2Deg;
        //image.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        //image.transform.LookAt(new Vector2(3.8f,3.2f));
        if (runningSpeed != 0)
        {
            //rotate image effect
            Vector2 velocity = transform.InverseTransformDirection(rb.velocity);
            image.transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z+velocity.y*1.3f*direction);
            
            print(transform.position.y);
              
            transform.Translate(new Vector2(runningSpeed*Time.deltaTime*direction,0));
        

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
        ScoreCenter.Reset();
        SceneManager.LoadScene(sceneName);
        print("die");
    }

    void Jump()
    {
        float x = transform.InverseTransformDirection(rb.velocity).x;
        rb.velocity = transform.TransformDirection(new Vector2(x,jumpPower*Time.deltaTime));
        isTouchFloor = false;
        needToJump = false;
        isJumping = true;
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

    IEnumerator PlaceCamera()
    {
        while (true)
        {
            yield return null;

            Debug.DrawRay(bottom.transform.position, transform.TransformDirection(new Vector2(0,-5.0f)),Color.red);
            hit = Physics2D.Raycast(bottom.transform.position,transform.TransformDirection(new Vector2(0,-1)), 5.0f);
            if (hit) 
            {
                CameraTarget.transform.rotation = transform.rotation;
                Vector2 position = transform.InverseTransformPoint(new Vector2(hit.point.x,hit.point.y));
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

    IEnumerator GetTouch()
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
}
