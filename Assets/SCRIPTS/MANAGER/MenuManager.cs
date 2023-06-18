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

    public void SetupP1vsP2()
    {
        SceneManager.LoadScene("GAME");
    }
}
