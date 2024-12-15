using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyFlip : MonoBehaviour
{
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        sp.flipX = mousePos.x < transform.position.x;
    }
}
