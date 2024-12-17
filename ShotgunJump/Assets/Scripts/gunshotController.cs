using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunshotController : MonoBehaviour
{
    private float timer = .417f;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }

        transform.position =  gameObject.GetComponentInParent<Transform>().position;
    }
}
