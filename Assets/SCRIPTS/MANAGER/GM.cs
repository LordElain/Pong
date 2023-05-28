using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    #region GameRunning

    [SerializeField] private bool GameRunning;
    [SerializeField] private Ball ball;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GameRunning = true;
        ball.chooseDirection();
        ball.GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRunning)
        {
           
        }
    }

    void Goal()
    {
        ball.Reset();
    }
}
