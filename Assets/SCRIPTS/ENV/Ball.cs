using MANAGER;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ENV
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] public float ballSpeed;
        [SerializeField] private Enum.PlayerSide chosenSide;
        [SerializeField] private string lastPlayerContact;
        [SerializeField] private Gm gameManager;

        public Rigidbody2D rb;
    
        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public Enum.PlayerSide ChooseDirection()
        {
            return Random.Range(0, 2) == 0 ? Enum.PlayerSide.Left : Enum.PlayerSide.Right;
        }

        public void Reset()
        {
            transform.position = Vector2.zero;
        }

        public void Movement()
        {
            chosenSide = ChooseDirection();
            var coordY = Random.Range(-1, 2); 
        
            if (chosenSide == Enum.PlayerSide.Right)
            {
                rb.velocity = new Vector2(1, coordY) * ballSpeed;
            }
            else
            {
                rb.velocity = new Vector2(-1, coordY) * ballSpeed;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        { 
            if (col.gameObject.name.Contains("PLAYER"))
            {
                lastPlayerContact = col.gameObject.name;
                gameManager.lastContactPlayer = GetLastPlayer();
            }


        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("POWERUP"))
            {
                gameManager.powerUpUsed = true;
            }
        }

        private Enum.PlayerVariation GetLastPlayer()
        {
            return lastPlayerContact.Contains("2") ? Enum.PlayerVariation.P2 : Enum.PlayerVariation.P1;
        }
    }
}
