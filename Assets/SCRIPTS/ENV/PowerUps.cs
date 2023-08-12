using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace.ENV
{
    public class PowerUps : MonoBehaviour
    {
        public ENUM.PlayerVariation lastContactPlayer;

        public GameObject powerUp;
        [SerializeField] private int activePowerUpID;
        [SerializeField] private ENUM.PlayerVariation powerUpUser;
        [SerializeField] private List<int> powerUpIDs = new List<int>();

        
        public int p1Vel;
        public int p2Vel;
        [SerializeField] private Vector3 p1Size;
        [SerializeField] private Vector3 p2Size;
        [SerializeField] private float ballVel;
        
        [Header("PowerUp Values")]
        #region powerUpValues
    
        [SerializeField] private int powerUpSpeed;
        [SerializeField] private float powerUpSize;
    
        private SpriteRenderer _ballSprite;
        #endregion
        
        [SerializeField] private GM _gm;

        private void Start()
        {
            SetupOGValues();
            _ballSprite = _gm.ball.GetComponent<SpriteRenderer>();
        }

        public void RandomPowerUp()
        {
            _gm.powerUpUsed = false;
            powerUp.SetActive(false);
            _gm.powerUpActive = true;
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

    public void ResetPowerUp()
    {
        _gm.powerUpActive = false;
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
                _gm.player1.playerSpeed = p1Vel;
            }
            else
            {
                _gm.player2.playerSpeed = p2Vel;
            }
        }
        else
        {
            if (lastContactPlayer == 0)
            {
                _gm.player1.playerSpeed = powerUpSpeed;
            }
            else
            {
                _gm.player2.playerSpeed = powerUpSpeed;
            }
        }
    }

    void PowerUp_Size(bool Resetstatus)
    {
        if (Resetstatus)
        {
            if (powerUpUser == 0)
            {
                _gm._gameObject_player.transform.localScale = p1Size;
            }
            else
            {
                _gm._gameObject_player2.transform.localScale = p2Size;
            }
        }
        else
        {
            if (powerUpUser == 0)
            {
                _gm._gameObject_player.transform.localScale += new Vector3(0, powerUpSize, 0);
            }
            else
            {
                _gm._gameObject_player2.transform.localScale += new Vector3(0, powerUpSize, 0);
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
    
    void SetupOGValues()
    {
        _gm.player1.playerSpeed = PlayerPrefs.GetInt("P1Speed");
        _gm.player2.playerSpeed = PlayerPrefs.GetInt("P2Speed");
        p1Vel = _gm.player1.playerSpeed;
        p2Vel = _gm.player2.playerSpeed;
        p1Size = _gm._gameObject_player.transform.localScale;
        p2Size = _gm._gameObject_player2.transform.localScale;
        ballVel = _gm.ball.ballSpeed;
    }
    }
}