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
    [SerializeField] private bool isPlayer1;

    private Control newPlayerInput;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        newPlayerInput = new Control();
        playerPos_X = 0;
        newPlayerInput.Enable();
        if (isPlayer1)
        {
            newPlayerInput.Gameplay.Movement.performed += moving =>
            {
                playerPos_Y = moving.ReadValue<float>();
            };
        }
        else
        {
            newPlayerInput.Gameplay.Movement2.performed += moving =>
            {
                playerPos_Y = moving.ReadValue<float>();
            };
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition(playerPos_X, playerPos_Y);
    }

    void UpdatePosition(float x, float y)
    {
        rb.velocity = new Vector2(x, y * playerSpeed);
        //((Vector2)transform.position + (new Vector2(0, y) * (playerSpeed * Time.deltaTime)))
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "WALL")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
