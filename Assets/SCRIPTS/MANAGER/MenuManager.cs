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

        for (int i = 0; i < Book.Count; i++)
        {
            Book[i].SetActive(false);
        }
        
        Book[0].SetActive(true);
    }

    public void SetupP1vsP2(string GameMode)
    {
        switch (GameMode)
        {
            //Logic: Asking if Player is AI, 0 - False, 1 - True
            case "P1VSP2":
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 0);
                
                Book[0].SetActive(false);
                Book[2].SetActive(true);
                setSpeedP1(3);
                setSpeedP2(3);
                break;
            case "P1VSAI":
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 1);
                
                Book[0].SetActive(false);
                Book[1].SetActive(true);
                Book[2].SetActive(false);
                break;
            case "AIVSAI":
                PlayerPrefs.SetInt("P1", 1);
                PlayerPrefs.SetInt("P2", 1);
                SceneManager.LoadScene("GAME");
                break;
            
            default:
                PlayerPrefs.SetInt("P1", 0);
                PlayerPrefs.SetInt("P2", 0);
                Book[0].SetActive(false);
                Book[2].SetActive(true);
                break;
        }
        
    
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack(int page)
    {
        switch (page)
        {
            case 0: 
                Book[0].SetActive(true);
                break;
            case 1:
                Book[page].SetActive(false);
                Book[0].SetActive(true);
                break;
            case 2: 
                Book[page].SetActive(false);
                Book[0].SetActive(true);
                break;
            default: 
                Book[0].SetActive(true);
                break;
        }

    }

    public void setSpeedP2(int Speed)
    {
        PlayerPrefs.SetInt("P2Speed", Speed);
    }
    public void setSpeedP1(int Speed)
    {
        PlayerPrefs.SetInt("P1Speed", Speed);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("GAME");
    }

    public void setGameMode(int Mode)
    {
        // 0 - Classic, 1 - Modern
        PlayerPrefs.SetInt("GameMode", Mode);
    }
}
