using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private bool isGrounded = false;
    private float groundY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        isGrounded = true;
        
    }

    void OnTriggerExit2D()
    {
        isGrounded  = false;
    }

    public bool GetGroundCheck()
    {
        return isGrounded;
    }
}
