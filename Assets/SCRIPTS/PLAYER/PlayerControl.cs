using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerPos_X;
    [SerializeField] private float playerPos_Y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("up") || Input.GetKey("down"))
        {
            playerPos_X = 0;
            playerPos_Y = Input.GetAxis("Vertical");
            UpdatePosition(playerPos_X, playerPos_Y);
        }
    }

    void UpdatePosition(float x, float y)
    {
        transform.Translate(new Vector2(x,y) * (playerSpeed * Time.deltaTime));
    }
}
