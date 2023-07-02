using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] public float ballSpeed;
    [SerializeField] private int chosenSide;
    [SerializeField] private string lastPlayerContact;
    [SerializeField] private GM gameManager;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int chooseDirection()
    {
        return Random.Range(0, 2);
        //0 - Left, 1 - Right
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
    }

    public void Movement()
    {
        chosenSide = chooseDirection();
        var coordY = Random.Range(-1, 2); 
        
        if (chosenSide == 1)
        {
            rb.velocity = new Vector2(1, coordY) * ballSpeed;
        }
        else
        {
            rb.velocity = new Vector2(-1, coordY) * ballSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.name.Contains("PLAYER"))
        {
            lastPlayerContact = col.gameObject.name;
            gameManager.lastContactPlayer = GetLastPlayer();
        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("POWERUP"))
        {
            gameManager.powerUpUsed = true;
        }
    }

    private int GetLastPlayer()
    {
        //P1 - 0, P2 - 1
        return lastPlayerContact.Contains("2") ? 1 : 0;
    }
}
