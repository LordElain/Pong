using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    #region GameRunning

    [SerializeField] private bool GameRunning;
    [SerializeField] private Ball ball;

    #endregion

    #region UI

    private int scoreP1;
    private int scoreP2;

    #endregion

    [SerializeField] private GameObject wall_Left;
    [SerializeField] private GameObject wall_Right;
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

    public void Goal(string WallName)
    {
        addPoints(WallName);
        ball.Reset();
    }

    void addPoints(string WallName)
    {
        if (WallName.Contains("LEFT"))
        {
            scoreP1++;
        }
        else
        {
            scoreP2++;
        }
    }
}
