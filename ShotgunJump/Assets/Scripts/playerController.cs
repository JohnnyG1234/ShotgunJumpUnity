using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private GameObject feetHitbox;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpTime;
    [SerializeField]
    private float jumpNum;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    float behopTimer;
    [SerializeField]
    float behopSpeedInc;
    [SerializeField]
    float  shotgunJumpTime;
    [SerializeField]
    UnityEngine.Vector2 shotgunForce;
    


    private float dir;
    private bool shouldJump = false;
    private float jumpStarted;
    private float airTime = 0f;
    private float groundTime = 0f;
    private float currentSpeed;
    private float groundDistance;
    private UnityEngine.Vector3 mousePos;
    private float currentShotgunTime;
    UnityEngine.Vector2 shotGunDir;



    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        //sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //crosshair stuff
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dir = Input.GetAxis("Horizontal");
        bool grounded = feetHitbox.GetComponent<groundCheck>().GetGroundCheck();

        if (Input.GetButtonDown("Jump") & grounded | Input.GetAxis("Mouse ScrollWheel") < 0f & grounded)
        {
            shouldJump = true;
            jumpStarted = jumpTime;
            behop();
            groundTime = 0f;
        }

        // update ground time
        if (grounded)
        {
            airTime = 0;
            groundTime += Time.deltaTime;
        }
        else
        {
            groundTime = 0;
            airTime += Time.deltaTime;
        }
        

        if (Input.GetButtonDown("Fire1"))
        {
            SHOTGUN();
        }
        
        groundDistance = feetHitbox.GetComponent<groundCheck>().GetGroundY();
    }

    void FixedUpdate()
    {   
        //move that player yo
        float newY = calcGravity();
        float newX = moveX();

        // shotgun!!!!
        if (currentShotgunTime > 0)
        {
            currentShotgunTime -= Time.deltaTime;
            newX += shotgunForce.x  * shotGunDir.x;
            newY += shotgunForce.y  * shotGunDir.y;
        }
        gameObject.transform.position = new UnityEngine.Vector2(newX, newY);

        // jumping
        if (shouldJump | jumpStarted > 0)
        {
            Jump();
            shouldJump = false;
            jumpStarted -= Time.deltaTime;
            airTime += Time.deltaTime;
        }



        // reset speed if we don't behop in time
        if  (groundTime > behopTimer)
        {
            currentSpeed = speed;
        }
    }

    private void Jump()
    {
        float newY = gameObject.transform.position.y + jumpNum;
        gameObject.transform.position = new UnityEngine.Vector2(gameObject.transform.position.x, newY);
    }

    private void behop()
    {
        // if we jump in time increase speed up to max 
        if  (groundTime < behopTimer)
        {
            currentSpeed += behopSpeedInc;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
    }

    private float moveX()
    {
        float newX = gameObject.transform.position.x;

        UnityEngine.Vector3 rightOffsett = new UnityEngine.Vector3(.5f,0,0);
        UnityEngine.Vector3 leftOffsett = new UnityEngine.Vector3(-.5f,0,0);


        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + rightOffsett, UnityEngine.Vector2.right);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + leftOffsett, UnityEngine.Vector2.left);

        if (hitRight)
        {   
            if (hitRight.distance < .05 & dir > 0 & hitRight.collider.gameObject.tag == "floor")
            {
                return newX;
            }
        }
        
        if (hitLeft)
        {
            if (hitLeft.distance < .05 & dir < 0 & hitLeft.collider.gameObject.tag == "floor")
            {
                return newX;
            }
        }

        newX += currentSpeed * dir;
        return newX;
    }


    private float calcGravity()
    {
        float newY = gameObject.transform.position.y;
        if (!feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            //gravity
            newY = gameObject.transform.position.y - gravity - Mathf.Min(airTime,1);
            
            // but don't go below the ground
            float groundY = gameObject.transform.position.y - groundDistance - .55f; //feetHitbox.GetComponent<groundCheck>().GetGroundY() + 
            if (groundY > newY)
            {
                newY = groundY;
            }
        }
        else
        {
            airTime = 0;
        }

        return newY;
    }

    private void SHOTGUN()
    {
        shotGunDir = new UnityEngine.Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        shotGunDir = -shotGunDir.normalized;
        currentShotgunTime = shotgunJumpTime;
    }
}
