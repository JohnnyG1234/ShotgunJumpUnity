using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private bool isGrounded = true;
    private float groundY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "floor")
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
        return groundY;
    }
}
