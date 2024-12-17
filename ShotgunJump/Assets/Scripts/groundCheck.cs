using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private bool isGrounded = true;
    private float groundDistance;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        //raycasting to get ground distance
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, UnityEngine.Vector2.down);
        groundDistance = groundHit.distance;
        if (groundDistance <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Debug.Log(isGrounded);
    }

    public bool GetGroundCheck()
    {
        return isGrounded;
    }


    public float GetGroundY()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, UnityEngine.Vector2.down);
        groundDistance = groundHit.distance;
        return groundDistance;
    }
}
