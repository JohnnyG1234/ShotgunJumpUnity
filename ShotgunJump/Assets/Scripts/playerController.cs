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
    private float landLock;


    private float dir;
    private bool shouldJump = false;
    private float jumpStarted;
    private float airTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") & feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            shouldJump = true;
            jumpStarted = jumpTime;
            
        }
    }

    void FixedUpdate()
    {   
        float newY = gameObject.transform.position.y;
        float newX = gameObject.transform.position.x;

        if (!feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            //gravity
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(UnityEngine.Vector2.down),landLock);
            if (hit)
            {
                newY = gameObject.transform.position.y - landLock;
            }
            else
            {
                newY = gameObject.transform.position.y - gravity - Mathf.Min(airTime,2);
            }
            
        }
        else
        {
            airTime = 0;
        }

        newX += speed * dir;
        gameObject.transform.position = new UnityEngine.Vector2(newX, newY);

        if (shouldJump | jumpStarted > 0)
        {
            Jump();
            shouldJump = false;
            jumpStarted -= Time.deltaTime;
            airTime += Time.deltaTime;
        }
    }

    private void Jump()
    {
        float newY = gameObject.transform.position.y + jumpNum;
        gameObject.transform.position = new UnityEngine.Vector2(gameObject.transform.position.x, newY);
    }

}
