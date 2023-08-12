using ENV;
using PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace MANAGER
{
    public class Gm : MonoBehaviour
    {

        [Header("GameRunning")]
        #region GameRunning
    
        public Ball ball;
    
        [FormerlySerializedAs("GameMode")]
        [Range(0,1)]
        [SerializeField] private Enum.GameMode gameMode;

        #endregion

        [Header("UI")]
        #region UI
    
        private int _scoreP1;
        private int _scoreP2;
        [Tooltip("Resolution of screen")]
        [SerializeField] private Vector3 stageDimensions;
        [SerializeField] private GameObject canvasPauseMenu;
        
        [FormerlySerializedAs("scoreP1_Text")] [SerializeField] private Text scoreP1Text;
        [FormerlySerializedAs("scoreP2_Text")] [SerializeField] private Text scoreP2Text;

        [SerializeField] private MenuManager menuManager;
        #endregion

        [FormerlySerializedAs("wall_Left")]
        [Header("Player field")]
        #region Playfield
    
        [SerializeField] private GameObject wallLeft;
        [FormerlySerializedAs("wall_Right")] [SerializeField] private GameObject wallRight;
        [FormerlySerializedAs("wall_Up")] [SerializeField] private GameObject wallUp;
        [FormerlySerializedAs("wall_Down")] [SerializeField] private GameObject wallDown;
        [FormerlySerializedAs("_gameObject_player")] public GameObject gameObjectPlayer;
        [FormerlySerializedAs("_gameObject_player2")] public GameObject gameObjectPlayer2;
    
        #endregion
    
        [Header("Player")]
        #region Player

        public PlayerControl player1;
        public PlayerControl player2;


        #endregion
    
        [Header("PowerUp")]
        #region powerUp
    
        [SerializeField] public Enum.PlayerVariation lastContactPlayer;
        public bool powerUpUsed;
        public bool powerUpActive;
        [SerializeField] private float powerUpSpawnTimer; 
        [SerializeField] private float powerUpSkillTimer;
    
        [SerializeField] private float powerUpCooldown; 
        [SerializeField] private float powerUpSpawnCooldown;

        [FormerlySerializedAs("_powerUps")] [SerializeField] private PowerUps powerUps;
        [FormerlySerializedAs("_gameObjectPowerUP")] [SerializeField] private GameObject gameObjectPowerUp;
    
        [TextArea ()] 
        [SerializeField] private string testTextArea;

        #endregion


        // Start is called before the first frame update1
        void Start()
        {
            menuManager.SetupBackground();
            if (Camera.main != null)
            {
                stageDimensions = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            }
            
            if (gameMode == Enum.GameMode.Modern)
            {
                SpawnPowerUp();
            }
            
            SetupPosition();
            Setup();
   
            gameMode = (Enum.GameMode)PlayerPrefs.GetInt("GameMode");
            ball.ChooseDirection();
            ball.Movement();
        }

        // Update is called once per frame
        void Update()
        {
            if (!player1.paused)
            {
                UnpauseMenu();
                if (gameMode != Enum.GameMode.Modern) return;
            
                /*PowerUp Spawn logic*/
                if (!powerUpActive)
                {
                    if (!gameObjectPowerUp.activeSelf)
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
                        powerUps.RandomPowerUp();
                    }
                }

                else
                {
                    powerUpSkillTimer += Time.deltaTime;
                    if (powerUpSkillTimer < powerUpCooldown) return;
                    powerUpSkillTimer = 0;
                    powerUps.ResetPowerUp();
                }
            }
            else
            {
                PauseMenu();
            }
        }
    
        public void Goal(string wallName)
        {
            AddPoints(wallName);
            ball.Reset();
            ball.Movement();
        }

        void AddPoints(string wallName)
        {
            if (wallName.Contains("LEFT"))
            {
                _scoreP1++;
            }
            else
            {
                _scoreP2++;
            }
            UpdateUI();
        }

        void SetupPosition()
        {
            wallLeft.transform.position = new Vector3(-stageDimensions.x - 0.5f, 0, 0);
            wallRight.transform.position = new Vector3(stageDimensions.x + 0.5f, 0,0);
            wallUp.transform.position = new Vector3(0,stageDimensions.y - 0.2f,0);
            wallDown.transform.position = new Vector3(0, -stageDimensions.y + 0.2f, 0);

            gameObjectPlayer.transform.position = new Vector3(stageDimensions.x - 1f, 0, 0);
            gameObjectPlayer2.transform.position = new Vector3(-stageDimensions.x + 1f, 0, 0);
        }

        void UpdateUI()
        {
            scoreP1Text.text = _scoreP1.ToString();
            scoreP2Text.text = _scoreP2.ToString();
        }

        void Setup()
        {
            var p1 = PlayerPrefs.GetInt("P1");
            var p1Speed = PlayerPrefs.GetInt("P1Speed");
        
            if (p1 == 0)
            {
                player1.isPlayer1 = true;
                player1.isPlayerAi = false;
            }
            else
            {
                player1.isPlayer1 = true;
                player1.isPlayerAi = true;
            }

            player1.playerSpeed = p1Speed;
            
            var p2 = PlayerPrefs.GetInt("P2");
            var p2Speed = PlayerPrefs.GetInt("P2Speed");
            
            if (p2 == 0)
            {
                player2.isPlayer1 = false;
                player2.isPlayerAi = false;
            }
            else
            {
                player2.isPlayer1 = false;
                player2.isPlayerAi = true;
            }

            player2.playerSpeed = p2Speed;
        }

        void PauseMenu()
        {
            Time.timeScale = 0;
            canvasPauseMenu.SetActive(true);
        }

        public void UnpauseMenu()
        {
            Time.timeScale = 1;
            player1.paused = false;
            canvasPauseMenu.SetActive(false);
        }

        public void QuitGame()
        {
            SceneManager.LoadScene("MAIN MENU");
        }
    
        [ContextMenu("Spawn")]
        void SpawnPowerUp()
        {
            /*The magic numbers are used so the powerUP is not spawned in either walls*/
            var coordX = Random.Range(-stageDimensions.x + 1.5f, stageDimensions.x - 1.5f);
            var coordY = Random.Range(-stageDimensions.y + 1.5f, stageDimensions.y - 1.5f); 
        
            powerUps.transform.position = new Vector3(coordX, coordY, 0);
            gameObjectPowerUp.SetActive(true);
        }
    }
}
