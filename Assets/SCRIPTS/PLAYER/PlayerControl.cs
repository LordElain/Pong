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
    [SerializeField] private Vector2 playerPos;
    [SerializeField] private bool isPlayer1;
    [SerializeField] private bool isPlayerAi;
    [SerializeField] private float lerpSpeed;

    private Control newPlayerInput;

    [SerializeField] private Ball ball;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        newPlayerInput = new Control();
        playerPos_X = 0;
        playerPos = new Vector2(playerPos_X, playerPos_Y);
        newPlayerInput.Enable();
        
        if (isPlayer1)
        {
            newPlayerInput.Gameplay.Movement.performed += moving =>
            {
                playerPos.y = moving.ReadValue<float>();
            };
        }
        else
        {
            if(!isPlayerAi)
            {
                newPlayerInput.Gameplay.Movement2.performed += moving =>
                {
                    playerPos.y = moving.ReadValue<float>();
                };
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition(playerPos);
        if (isPlayerAi)
        {
            if (ball.transform.position.y > transform.position.y)
            {
                if (rb.velocity.y < 0) rb.velocity = Vector2.zero;
                playerPos = Vector2.up;
                UpdatePosition(playerPos);
            }
            else
            {
                if (rb.velocity.y > 0) rb.velocity = Vector2.zero;
                playerPos = Vector2.down;
                UpdatePosition(playerPos);
            }
            
        }
    }

    void UpdatePosition(Vector2 PlayerPos)
    {
        rb.velocity = Vector2.Lerp(rb.velocity, PlayerPos * playerSpeed, lerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "WALL")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
