using MANAGER;
using UnityEngine;

public class WallTracking : MonoBehaviour
{
    [SerializeField] private Gm gm;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BALL"))
        {
            gm.Goal(gameObject.name);
        }
    }
}
