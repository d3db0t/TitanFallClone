using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loadoutMenu;
    public GameObject gameManager;
    //public GameObject htpMenu;
    //public GameObject creditsMenu;

    void Update()
    {
        
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        loadoutMenu.SetActive(true);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
       
    }

    public void Credits()
    {

    }

    public void optionsBack()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void startBack()
    {
        loadoutMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("ParkourScene");
    }
}
