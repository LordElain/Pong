using System;
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


    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.MovePosition((Vector2)transform.position + (new Vector2(0, y) * (playerSpeed * Time.deltaTime)));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "WALL")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
