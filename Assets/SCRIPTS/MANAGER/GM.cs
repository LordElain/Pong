using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.ENV;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    [Header("GameRunning")]
    #region GameRunning
    
    public Ball ball;
    
    [Range(0,1)]
    [SerializeField] private ENUM.GameMode GameMode;

    #endregion

    [Header("UI")]
    #region UI
    
    private int scoreP1;
    private int scoreP2;
    [Tooltip("Resolution of screen")]
    [SerializeField] private Vector3 stageDimensions;
    [SerializeField] private GameObject canvasPauseMenu;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Text scoreP1_Text;
    [SerializeField] private Text scoreP2_Text;

    [SerializeField] private MenuManager menuManager;
    #endregion

    [Header("Playerfield")]
    #region Playfield
    
    [SerializeField] private GameObject wall_Left;
    [SerializeField] private GameObject wall_Right;
    [SerializeField] private GameObject wall_Up;
    [SerializeField] private GameObject wall_Down;
    public GameObject _gameObject_player;
    public GameObject _gameObject_player2;
    
    #endregion
    
    [Header("Player")]
    #region Player

    public PlayerControl player1;
    public PlayerControl player2;


    #endregion
    
    [Header("PowerUp")]
    #region powerUp
    
    [SerializeField] public ENUM.PlayerVariation lastContactPlayer;
    public bool powerUpUsed;
    public bool powerUpActive;
    [SerializeField] private float powerUpSpawnTimer; 
    [SerializeField] private float powerUpSkillTimer;
    
    [SerializeField] private float powerUpCooldown; 
    [SerializeField] private float powerUpSpawnCooldown;

    [SerializeField] private PowerUps _powerUps;
    [SerializeField] private GameObject _gameObjectPowerUP;
    
    [TextArea ()] 
    [SerializeField] private string test;

    #endregion


    // Start is called before the first frame update1
    void Start()
    {
        menuManager.SetupBackground();
        stageDimensions = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
       
        if (GameMode == ENUM.GameMode.Modern)
        {
            SpawnPowerUp();
        }
            
        SetupPosition();
        Setup();
   
        GameMode = (ENUM.GameMode)PlayerPrefs.GetInt("GameMode");
        ball.chooseDirection();
        ball.Movement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.Paused)
        {
            UnpauseMenu();
            if (GameMode != ENUM.GameMode.Modern) return;
            if (!powerUpActive)
            {
                if (!_gameObjectPowerUP.activeSelf)
                {
                    powerUpSpawnTimer += Time.deltaTime;
                    
                    if (powerUpSpawnTimer >= (powerUpSpawnCooldown + Random.Range(0,4)))
                    {
                        powerUpSpawnTimer = 0;
                        SpawnPowerUp();
                    }
                }
                
                if (powerUpUsed)
                {
                    _powerUps.RandomPowerUp();
                }
            }

            else
            {
                powerUpSkillTimer += Time.deltaTime;
                if (powerUpSkillTimer < powerUpCooldown) return;
                powerUpSkillTimer = 0;
                _powerUps.ResetPowerUp();
            }
        }
        else
        {
            PauseMenu();
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

    void PauseMenu()
    {
        Time.timeScale = 0;
        canvasPauseMenu.SetActive(true);
    }

    public void UnpauseMenu()
    {
        Time.timeScale = 1;
        player1.Paused = false;
        canvasPauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MAIN MENU");
    }
    
    [ContextMenu("Spawn")]
    void SpawnPowerUp()
    {
        var coordX = Random.Range(-stageDimensions.x + 2, stageDimensions.x - 1);
        var coordY = Random.Range(-stageDimensions.y + 1, stageDimensions.y); 
        
        _powerUps.transform.position = new Vector3(coordX, coordY, 0);
        _gameObjectPowerUP.SetActive(true);
    }
}
