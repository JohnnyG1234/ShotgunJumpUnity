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
    [SerializeField]
    private float shotGunMult;
    


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
    private float lastDir;



    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        //crosshair stuff
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = Input.GetAxisRaw("Horizontal");


        // check if we are moving in the same direction as last frame, if not reset speed
        if (dir != lastDir)
        {
            currentSpeed = speed;
        }

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
            groundTime += Time.deltaTime;
        }
        else
        {
            groundTime = 0;
            airTime += Time.deltaTime;
        }

        //Debug.Log(currentSpeed);
        

        if (Input.GetButtonDown("Fire1"))
        {
            SHOTGUN();
            airTime = 0;
        }

        lastDir = dir;
    }

    void FixedUpdate()
    {   
        groundDistance = feetHitbox.GetComponent<groundCheck>().GetGroundY();
        //move that player yo
        float newY = calcGravity();
        float newX = moveX();

        // shotgun!!!!
        if (currentShotgunTime > 0)
        {
            currentShotgunTime -= Time.deltaTime;
            newX += shotgunX();
            newY += shotgunY();
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
        UnityEngine.Vector3 Offsett = new UnityEngine.Vector3(0,.7f,0);
        RaycastHit2D hitup = Physics2D.Raycast(transform.position + Offsett, UnityEngine.Vector2.up);

        if (hitup)
        {
            if (jumpNum > hitup.distance &  hitup.transform.gameObject.tag != "Player")
            {
                jumpStarted = 0;
                newY = gameObject.transform.position.y + hitup.distance;
            }
            }
            
        gameObject.transform.position = new UnityEngine.Vector2(gameObject.transform.position.x, newY);
    }

    private void behop()
    {
        // if we jump in time increase speed up to max 
        if  (groundTime < behopTimer & dir != 0)
        {
            currentSpeed += behopSpeedInc;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
        else
        {
            currentSpeed  = speed;
        }
    }

    private float moveX()
    {
        
        float newX = gameObject.transform.position.x;
        //checking middle
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


        // checking high
        rightOffsett = new UnityEngine.Vector3(.5f , .5f,0);
        leftOffsett = new UnityEngine.Vector3(-.5f , .5f,0);


        hitRight = Physics2D.Raycast(transform.position + rightOffsett, UnityEngine.Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + leftOffsett, UnityEngine.Vector2.left);

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

        //checking low
        rightOffsett = new UnityEngine.Vector3(.5f, -.5f,0);
        leftOffsett = new UnityEngine.Vector3(-.5f, -.5f,0);


        hitRight = Physics2D.Raycast(transform.position + rightOffsett, UnityEngine.Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + leftOffsett, UnityEngine.Vector2.left);

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
            newY = gameObject.transform.position.y - gravity - Mathf.Min(airTime  * .5f,.6f);
            
            // but don't go below the ground
            float groundY = gameObject.transform.position.y - groundDistance - .2f;
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

    private float shotgunX()
    {


        if (shotGunDir.x > 0)
        {
            UnityEngine.Vector3 rightOffsett = new UnityEngine.Vector3(.5f,0,0);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + rightOffsett, UnityEngine.Vector2.right);
            
            if (hitRight)
            {
                if ((shotgunForce.x - currentShotgunTime * shotGunMult)  * shotGunDir.x > hitRight.distance)
                {
                    currentShotgunTime = 0;
                    return hitRight.distance;
                }
            }

        }
        else if (shotGunDir.x < 0)
        {
            UnityEngine.Vector3 leftOffsett = new UnityEngine.Vector3(-.5f,0,0);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + leftOffsett, UnityEngine.Vector2.left);

            if (hitLeft)
            {
                if (Mathf.Abs((shotgunForce.x - currentShotgunTime * shotGunMult)  * shotGunDir.x) > hitLeft.distance)
                {
                    currentShotgunTime = 0;
                    return -hitLeft.distance;
                }
            }
        }
        
        return (shotgunForce.x - currentShotgunTime * shotGunMult)  * shotGunDir.x;
    }

    private float shotgunY()
    {
        if (shotGunDir.y  < 0)
        {
                if (Mathf.Abs((shotgunForce.y - currentShotgunTime * shotGunMult)  * shotGunDir.y) > groundDistance)
                {
                    currentShotgunTime = 0;
                    return -groundDistance;
                }
        }
        else if (shotGunDir.y  > 0)
        {
            UnityEngine.Vector3 Offsett = new UnityEngine.Vector3(0,.5f,0);
            RaycastHit2D hitup = Physics2D.Raycast(transform.position + Offsett, UnityEngine.Vector2.up);

            if (hitup)
            {
                if ((shotgunForce.y - currentShotgunTime * shotGunMult)  * shotGunDir.y > hitup.distance)
                {
                    currentShotgunTime = 0;
                    return hitup.distance;
                }
            }
        }
        return (shotgunForce.y - currentShotgunTime * shotGunMult)  * shotGunDir.y;
 
    }
}
