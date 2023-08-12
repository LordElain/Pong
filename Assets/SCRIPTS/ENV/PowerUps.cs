using System.Collections.Generic;
using MANAGER;
using UnityEngine;
using Random = UnityEngine.Random;


namespace ENV
{
    public class PowerUps : MonoBehaviour
    {
        public Enum.PlayerVariation lastContactPlayer;

        public GameObject powerUp;
        [SerializeField] private int activePowerUpID;
        [SerializeField] private Enum.PlayerVariation powerUpUser;
        [SerializeField] private List<int> powerUpIDs = new List<int>();

        
        public int p1Vel;
        public int p2Vel;
        [SerializeField] private Vector3 p1Size;
        [SerializeField] private Vector3 p2Size;
        
        [Header("PowerUp Values")]
        #region powerUpValues
    
        [SerializeField] private int powerUpSpeed;
        [SerializeField] private float powerUpSize;
    
        private SpriteRenderer _ballSprite;
        #endregion
        
        [SerializeField] private Gm gm;

        private void Start()
        {
            SetupOriginalValues();
            _ballSprite = gm.ball.GetComponent<SpriteRenderer>();
        }

        public void RandomPowerUp()
        {
            gm.powerUpUsed = false;
            powerUp.SetActive(false);
            gm.powerUpActive = true;
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
        }

    }

    public void ResetPowerUp()
    {
        gm.powerUpActive = false;
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
        }
    }

    void PowerUp_Speed(bool resetStatus)
    {
        if (resetStatus)
        {
            if (powerUpUser == 0)
            {
                gm.player1.playerSpeed = p1Vel;
            }
            else
            {
                gm.player2.playerSpeed = p2Vel;
            }
        }
        else
        {
            if (lastContactPlayer == 0)
            {
                gm.player1.playerSpeed = powerUpSpeed;
            }
            else
            {
                gm.player2.playerSpeed = powerUpSpeed;
            }
        }
    }

    void PowerUp_Size(bool resetStatus)
    {
        if (resetStatus)
        {
            if (powerUpUser == 0)
            {
                gm.gameObjectPlayer.transform.localScale = p1Size;
            }
            else
            {
                gm.gameObjectPlayer2.transform.localScale = p2Size;
            }
        }
        else
        {
            if (powerUpUser == 0)
            {
                gm.gameObjectPlayer.transform.localScale += new Vector3(0, powerUpSize, 0);
            }
            else
            {
                gm.gameObjectPlayer2.transform.localScale += new Vector3(0, powerUpSize, 0);
            }
        }
    }

    void PowerUp_Blink(bool resetStatus)
    {
        if (resetStatus)
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
    
    void SetupOriginalValues()
    {
        gm.player1.playerSpeed = PlayerPrefs.GetInt("P1Speed");
        gm.player2.playerSpeed = PlayerPrefs.GetInt("P2Speed");
        p1Vel = gm.player1.playerSpeed;
        p2Vel = gm.player2.playerSpeed;
        p1Size = gm.gameObjectPlayer.transform.localScale;
        p2Size = gm.gameObjectPlayer2.transform.localScale;
    }
    }
}