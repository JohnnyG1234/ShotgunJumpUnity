using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D defualtCursor;
    [SerializeField] private Texture2D[] reloadingCursor;
    [SerializeField] private int frameCount;
    [SerializeField] private float frameRate;
    
    private int currentFrame;
    private float frameTimer;
    private playerController player;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defualtCursor, new Vector2(0,0), CursorMode.Auto);

        player = gameObject.GetComponentInParent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.GetReload())
        {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0f)
            {
                frameTimer += frameRate;
                currentFrame = (currentFrame + 1) % frameCount;
                Cursor.SetCursor(reloadingCursor[currentFrame], new Vector2(0,0), CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(defualtCursor, new Vector2(0,0), CursorMode.Auto);
        }
    }
}
