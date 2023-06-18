using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject Page1;
    [SerializeField] private GameObject Page2;
    [SerializeField] private GameObject Page3;

    private List<GameObject> Book = new List<GameObject>();

    private void Start()
    {
        Book.Add(Page1);
        Book.Add(Page2);
        Book.Add(Page3);
    }

    public void SetupP1vsP2(string GameMode)
    {
        switch (GameMode)
        {
            //Logic: Asking if Player is AI, 0 - False, 1 - True
            case "P1VSP2":
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 0);
                break;
            case "P1VSAI":
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 1);
                break;
            case "AIVSAI":
                PlayerPrefs.SetInt("P1", 1);
                PlayerPrefs.SetInt("P2", 1);
                break;
            
            default:
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 0);
                break;
        }
        
        SceneManager.LoadScene("GAME");
    }

}
