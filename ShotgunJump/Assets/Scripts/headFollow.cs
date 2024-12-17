using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headFollow : MonoBehaviour
{
    private UnityEngine.Vector2 dir;
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
        dir = new UnityEngine.Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.right = dir;
        sp.flipY = mousePos.x < transform.position.x;
    }

    public Vector2 GetRight()
    {
        return transform.right;
    }
}
