using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class gunshotAnimController : MonoBehaviour
{
    [SerializeField] GameObject gunshot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunAnim()
    {
        Instantiate(gunshot, new Vector3(transform.position.x, transform.position.y, 0), quaternion.identity);
    }
}
