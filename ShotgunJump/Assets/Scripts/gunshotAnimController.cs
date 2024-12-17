using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class gunshotAnimController : MonoBehaviour
{
    [SerializeField] GameObject gunshot;
    [SerializeField] headFollow head;
    
    private UnityEngine.Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.right = head.GetRight();
    }

    public void PlayGunAnim()
    {
        Instantiate(gunshot, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
