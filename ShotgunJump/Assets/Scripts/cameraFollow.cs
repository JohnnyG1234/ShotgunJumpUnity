using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f,2f, -10f);
    [SerializeField] private float smoothTime = 0.06f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
