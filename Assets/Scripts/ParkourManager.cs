using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkourManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameoverScreen;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < 0)
        {
            camera.SetActive(true);
            //GameObject.Destroy(player);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameoverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void restart()
    {

    }

    public void mainMenu()
    {
        SceneManager.LoadScene (sceneName:"MainMenu");
    }
}
