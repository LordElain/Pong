using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    [SerializeField] private int chosenSide;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void chooseDirection()
    {
        chosenSide = Random.Range(0, 2);
        //0 - Left, 1 - Right
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
    }

    public void GameStart()
    {
        //rb.MovePosition((Vector2)transform.position + (new Vector2(0, y) * (playerSpeed * Time.deltaTime)));

        if (chosenSide == 1)
        {
            rb.velocity = Vector2.right * ballSpeed;
        }
        else
        {
            rb.velocity = Vector2.left * ballSpeed;
        }
    }

    public void Movement()
    {
        rb.velocity = new Vector2(-1, -1) * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

    }
}
