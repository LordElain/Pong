using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MANAGER
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject page1;
        [SerializeField] private GameObject page2;
        [SerializeField] private GameObject page3;
        [SerializeField] private GameObject page4;

        private List<GameObject> _book = new List<GameObject>();

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Image bImage;

        private void Start()
        {
            SetupBackground();
            _book.Add(page1);
            _book.Add(page2);
            _book.Add(page3);
            _book.Add(page4);

            foreach (var t in _book)
            {
                t.SetActive(false);
            }

            _book[0].SetActive(true);
        }

        public void SetupP1VSP2(string gameMode)
        {
            switch (gameMode)
            {
                //Logic: Asking if Player is AI, 0 - False, 1 - True
                case "P1VSP2":
                    PlayerPrefs.SetInt("P1", 0);
                    PlayerPrefs.SetInt("P2", 0);

                    _book[0].SetActive(false);
                    _book[2].SetActive(true);
                    SetSpeedP1(3);
                    SetSpeedP2(3);
                    break;

                case "P1VSAI":
                    PlayerPrefs.SetInt("P1", 0);
                    PlayerPrefs.SetInt("P2", 1);

                    _book[0].SetActive(false);
                    _book[1].SetActive(true);
                    _book[2].SetActive(false);
                    break;

                case "AIVSAI":
                    PlayerPrefs.SetInt("P1", 1);
                    PlayerPrefs.SetInt("P2", 1);
                    SceneManager.LoadScene("GAME");
                    break;

                case "Options":
                    _book[0].SetActive(false);
                    _book[3].SetActive(true);
                    break;

                default:
                    PlayerPrefs.SetInt("P1", 0);
                    PlayerPrefs.SetInt("P2", 0);
                    _book[0].SetActive(false);
                    _book[2].SetActive(true);
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
                    _book[0].SetActive(true);
                    break;
                case 1:
                    _book[page].SetActive(false);
                    _book[0].SetActive(true);
                    break;
                case 2:
                    _book[page].SetActive(false);
                    _book[0].SetActive(true);
                    break;
                case 3:
                    _book[page].SetActive(false);
                    _book[0].SetActive(true);
                    break;
                default:
                    _book[0].SetActive(true);
                    break;
            }
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public void SetSpeedP2(int speed)
        {
            PlayerPrefs.SetInt("P2Speed", speed);
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public void SetSpeedP1(int speed)
        {
            PlayerPrefs.SetInt("P1Speed", speed);
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene("GAME");
        }

        public void SetGameMode(int mode)
        {
            // 0 - Classic, 1 - Modern
            PlayerPrefs.SetInt("GameMode", mode);
        }

        public void SetupBackground()
        {
            var color = PlayerPrefs.GetString("BG");

            /*Deciding which color is primary color. Because of Unity docs it has to be x/255f*/
            float bigValue = 160 / 255f;
            float smlValue = 35 / 255f;
        
            /*Setting the value for the primary color in a switch expression while not using the "old" structure*/
            Color colorForCamera = color switch
            {
                "Red" => new Color(bigValue, smlValue, smlValue),
                "Green" => new Color(smlValue, bigValue, smlValue),
                "Blue" => new Color(smlValue, smlValue, bigValue),
                "Black" => Color.black,
                _ => Color.black
            };

            mainCamera.backgroundColor = colorForCamera;

            if (bImage != null)
            {
                bImage.color = colorForCamera;
                if (colorForCamera == Color.black)
                {
                    bImage.color = Color.white;
                }
            }
        }

        public void SetBackground(string color)
        {
            PlayerPrefs.SetString("BG", color);
            SetupBackground();
        }
    }
}