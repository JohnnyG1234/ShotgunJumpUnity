using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
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


    private float dir;
    private bool shouldJump = false;
    private float jumpStarted;
    private float airTime = 0f;
    private float groundTime = 0f;
    private float currentSpeed;



    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxis("Horizontal");
        bool grounded = feetHitbox.GetComponent<groundCheck>().GetGroundCheck();

        if (Input.GetButtonDown("Jump") & grounded)
        {
            shouldJump = true;
            jumpStarted = jumpTime;
            behop();
            groundTime = 0f;
        }

        if (grounded)
        {
            groundTime += Time.deltaTime;
        }

        Debug.Log(currentSpeed);
    }

    void FixedUpdate()
    {   
        float newY = gameObject.transform.position.y;
        float newX = gameObject.transform.position.x;

        if (!feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            //gravity
            newY = gameObject.transform.position.y - gravity - Mathf.Min(airTime,2);
            // but don't go below the ground
            float groundY = feetHitbox.GetComponent<groundCheck>().GetGroundY() + 3;
            if (groundY > newY)
            {
                newY = groundY;
            }
        }
        else
        {
            airTime = 0;
        }

        newX += currentSpeed * dir;
        gameObject.transform.position = new UnityEngine.Vector2(newX, newY);

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
}
