using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    [Header("GameRunning")]
    #region GameRunning
    
    [SerializeField] private Ball ball;

    private SpriteRenderer _ballSprite;
    [Range(0,1)]
    [SerializeField] private int GameMode;

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
    [SerializeField] private GameObject _gameObject_player;
    [SerializeField] private GameObject _gameObject_player2;
    
    #endregion
    
    [Header("Player")]
    #region Player

    [SerializeField] private PlayerControl player1;
    [SerializeField] private PlayerControl player2;
    [SerializeField] private int p1Vel;
    [SerializeField] private int p2Vel;
    [SerializeField] private Vector3 p1Size;
    [SerializeField] private Vector3 p2Size;
    [SerializeField] private float ballVel;

    #endregion
    
    [Header("PowerUp")]
    #region powerUp
    
    [SerializeField] public ENUM.PlayerVariation lastContactPlayer;
    [SerializeField] public bool powerUpUsed;
    
    [SerializeField] private GameObject powerUp;
    [SerializeField] private int activePowerUpID;
    [SerializeField] private ENUM.PlayerVariation powerUpUser;
    [SerializeField] private List<int> powerUpIDs = new List<int>();
    [SerializeField] private bool powerupActive;
    [SerializeField] private float powerUpSpawnTimer; 
    [SerializeField] private float powerUpSkillTimer;
    
    [SerializeField] private float powerUpBlinkTimer;
    [SerializeField] private float powerUpBlinkTimeStore;
    [SerializeField] private float powerUpBlinkTime;
    
    
    [SerializeField] private float powerUpCooldown; 
    [SerializeField] private float powerUpSpawnCooldown; 
    
    [TextArea ()] 
    [SerializeField] private string test;

    #endregion

    [Header("PowerUp Values")]
    #region powerUpValues
    
    [SerializeField] private int powerUpSpeed;
    [SerializeField] private float powerUpSize;
    

    #endregion
    // Start is called before the first frame update1
    void Start()
    {
        menuManager.SetupBackground();
        stageDimensions = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        SpawnPowerUp();
        SetupPosition();
        Setup();
        SetupOGValues();
        _ballSprite = ball.GetComponent<SpriteRenderer>();
        GameMode = PlayerPrefs.GetInt("GameMode");
        ball.chooseDirection();
        ball.Movement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.Paused)
        {
            UnpauseMenu();
            if (!powerupActive)
            {
                if (!powerUp.gameObject.activeSelf)
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
                    RandomPowerUp();
                }
            }

            else
            {
                powerUpSkillTimer += Time.deltaTime;
                if (powerUpSkillTimer >= powerUpCooldown)
                {
                    powerUpSkillTimer = 0;
                    ResetPowerUp();
                }
            }
        }
        else
        {
            PauseMenu();
        }
    }

    void SetupOGValues()
    {
        player1.playerSpeed = PlayerPrefs.GetInt("P1Speed");
        player2.playerSpeed = PlayerPrefs.GetInt("P2Speed");
        p1Vel = player1.playerSpeed;
        p2Vel = player2.playerSpeed;
        p1Size = _gameObject_player.transform.localScale;
        p2Size = _gameObject_player2.transform.localScale;
        ballVel = ball.ballSpeed;
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
        
        powerUp.transform.position = new Vector3(coordX, coordY, 0);
        powerUp.SetActive(true);
    }

    void RandomPowerUp()
    {
        powerUpUsed = false;
        powerUp.SetActive(false);
        powerupActive = true;
        var maxCount = powerUpIDs.Count;
        var activePowerUp = powerUpIDs[Random.Range(0, maxCount)];
        powerUpUser = lastContactPlayer;
        activePowerUpID = activePowerUp;
        
        switch (activePowerUp)
        {
            case 0: 
                PowerUp_Speed(false);
                break;        
            case 1:
                PowerUp_Size(false);
                break;
            case 2:
                PowerUp_Blink(false);
                break;
            default:
                break;
        }

    }

    void ResetPowerUp()
    {
        powerupActive = false;
        switch (activePowerUpID)
        {
            case 0: 
                PowerUp_Speed(true);
                break;
            case 1:
                PowerUp_Size(true);
                break;
            case 2:
                PowerUp_Blink(true);
                break;
            default:
                break;
        }
    }

    void PowerUp_Speed(bool Resetstatus)
    {
        if (Resetstatus)
        {
            if (powerUpUser == 0)
            {
                player1.playerSpeed = p1Vel;
            }
            else
            {
                player2.playerSpeed = p2Vel;
            }
        }
        else
        {
            if (lastContactPlayer == 0)
            {
                player1.playerSpeed = powerUpSpeed;
            }
            else
            {
                player2.playerSpeed = powerUpSpeed;
            }
        }
    }

    void PowerUp_Size(bool Resetstatus)
    {
        if (Resetstatus)
        {
            if (powerUpUser == 0)
            {
                _gameObject_player.transform.localScale = p1Size;
            }
            else
            {
                _gameObject_player2.transform.localScale = p2Size;
            }
        }
        else
        {
            if (powerUpUser == 0)
            {
                _gameObject_player.transform.localScale += new Vector3(0, powerUpSize, 0);
            }
            else
            {
                _gameObject_player2.transform.localScale += new Vector3(0, powerUpSize, 0);
            }
        }
    }

    void PowerUp_Blink(bool Resetstatus)
    {
        if (Resetstatus)
        {
                float alpha = 255;
                Color ballSpriteColor = _ballSprite.color;
                ballSpriteColor.a = alpha;
                _ballSprite.color = ballSpriteColor;
        }
        else
        {
            float alpha = 0;
            Color ballSpriteColor = _ballSprite.color;
            ballSpriteColor.a = alpha;
            _ballSprite.color = ballSpriteColor;
        }
    }
}
