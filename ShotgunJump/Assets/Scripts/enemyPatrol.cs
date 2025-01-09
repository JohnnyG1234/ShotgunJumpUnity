using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private float speed = .1f;
    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = pointA;
    }

    void FixedUpdate()
    {
        if (MathF.Abs(transform.position.x - currentTarget.position.x) < .5f)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }
        }

        if (transform.position.x < currentTarget.position.x)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.y);
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.y);
        }
    }
}
