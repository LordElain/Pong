using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTracking : MonoBehaviour
{
    [SerializeField] private GM gm;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BALL")
        {
            gm.Goal(gameObject.name);
        }
    }
}
