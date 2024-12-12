using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private GameObject feetHitbox;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float speed;


    private float dir;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {   
        float newY = gameObject.transform.position.y;
        float newX = gameObject.transform.position.x;

        if (!feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            //gravity
            newY = gameObject.transform.position.y - gravity;
        }

        newX += speed * dir;

        gameObject.transform.position = new UnityEngine.Vector2(newX, newY);

        
    }

    private void Jump()
    {
        
    }
}
