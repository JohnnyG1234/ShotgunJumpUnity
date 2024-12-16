using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private bool isGrounded = true;
    private float groundDistance;
    private UnityEngine.Vector3 groundOffset = new UnityEngine.Vector3(0, -.2f, 0);


    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        //raycasting to get ground distance
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position + groundOffset, UnityEngine.Vector2.down);
        groundDistance = groundHit.distance;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "floor" & groundDistance  < .05)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D()
    {
        isGrounded  = false;
    }

    public bool GetGroundCheck()
    {
        return isGrounded;
    }

    public float GetGroundY()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position + groundOffset, UnityEngine.Vector2.down);
        groundDistance = groundHit.distance;
        return groundDistance;
    }
}
