using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] public float ballSpeed;
    [SerializeField] private int chosenSide;

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

}
