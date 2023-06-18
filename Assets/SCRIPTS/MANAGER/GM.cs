using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject wall_Up;
    [SerializeField] private GameObject wall_Down;
    [SerializeField] private GameObject _gameObject_player;
    [SerializeField] private GameObject _gameObject_player2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Text scoreP1_Text;
    [SerializeField] private Text scoreP2_Text;

    [SerializeField] private PlayerControl player1;
    [SerializeField] private PlayerControl player2;
    // Start is called before the first frame update1
    void Start()
    {
        SetupPosition();
        Setup();
        GameRunning = true;
        ball.chooseDirection();
        ball.Movement();
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
        ball.Movement();
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
        updateUI();
    }

    void SetupPosition()
    {
        Vector3 stageDimensions = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        wall_Left.transform.position = new Vector3(-stageDimensions.x - 0.5f, 0, 0);
        wall_Right.transform.position = new Vector3(stageDimensions.x + 0.5f, 0,0);
        wall_Up.transform.position = new Vector3(0,stageDimensions.y - 0.2f,0);
        wall_Down.transform.position = new Vector3(0, -stageDimensions.y + 0.2f, 0);

        _gameObject_player.transform.position = new Vector3(stageDimensions.x - 1f, 0, 0);
        _gameObject_player2.transform.position = new Vector3(-stageDimensions.x + 1f, 0, 0);
    }

    void updateUI()
    {
        scoreP1_Text.text = scoreP1.ToString();
        scoreP2_Text.text = scoreP2.ToString();
    }

    void Setup()
    {
        var P1 = PlayerPrefs.GetInt("P1");
        
        if (P1 == 0)
        {
            player1.isPlayer1 = true;
            player1.isPlayerAi = false;
        }
        else
        {
            player1.isPlayer1 = true;
            player1.isPlayerAi = true;
        }

        var P2 = PlayerPrefs.GetInt("P2");
        
        if (P2 == 0)
        {
            player2.isPlayer1 = false;
            player2.isPlayerAi = false;
        }
        else
        {
            player2.isPlayer1 = false;
            player2.isPlayerAi = true;
        }
    }
}
