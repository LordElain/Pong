using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    #region GameRunning

    [SerializeField] private bool GameRunning;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
