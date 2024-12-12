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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {   
        if (feetHitbox.GetComponent<groundCheck>().GetGroundCheck())
        {
            //gravity
            float newY = gameObject.transform.position.y - gravity;
            gameObject.transform.position = new UnityEngine.Vector2(gameObject.transform.position.x, newY);
        }
    }
}
