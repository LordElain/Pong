using ENV;
using UnityEngine;
using UnityEngine.Serialization;

namespace PLAYER
{
    public class PlayerControl : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] public int playerSpeed;
        [FormerlySerializedAs("playerPos_X")] [SerializeField] private float playerPosX;
        [FormerlySerializedAs("playerPos_Y")] [SerializeField] private float playerPosY;
        [SerializeField] private Vector2 playerPos;
        [SerializeField] public bool isPlayer1;
        [SerializeField] public bool isPlayerAi;
        [FormerlySerializedAs("Paused")] [SerializeField] public bool paused;
        [SerializeField] private float lerpSpeed;

        private Control _newPlayerInput;

        [SerializeField] private Ball ball;

        private Rigidbody2D _rb;
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            paused = false;
            _newPlayerInput = new Control();
            playerPosX = 0;
            playerPos = new Vector2(playerPosX, playerPosY);
            _newPlayerInput.Enable();
        
            /* Control logic for movement */
            if (isPlayer1)
            {
                _newPlayerInput.Gameplay.Movement.performed += moving =>
                {
                    playerPos.y = moving.ReadValue<float>();
                };

                _newPlayerInput.Gameplay.Pause.performed += context =>
                {
                    paused = !paused;
                };
            }
            else
            {
                _newPlayerInput.Gameplay.Movement2.performed += moving =>
                {
                    playerPos.y = moving.ReadValue<float>();
                };
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UpdatePosition();
        
            /* The "Ai" is following the position of the ball and updating the playerPosition accordingly*/
            if (isPlayerAi)
            {
                if (ball.transform.position.y > transform.position.y)
                {
                    if (_rb.velocity.y < 0) _rb.velocity = Vector2.zero;
                    playerPos = Vector2.up;
                    UpdatePosition();
                }
                else
                {
                    if (_rb.velocity.y > 0) _rb.velocity = Vector2.zero;
                    playerPos = Vector2.down;
                    UpdatePosition();
                }
            
            }
        }

        void UpdatePosition()
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, playerPos * playerSpeed, lerpSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            /*Checking the collision with wall to simulate a stop*/
            if (col.transform.CompareTag("WALL"))
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }
}
