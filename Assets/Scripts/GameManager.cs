using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool SG = false;
    public static bool AR = false;
    public static bool SR = false;
    public static bool gamePaused = false;
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    private GameManager gameManager;
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }

    public void setShotgun()
    {
        SG = true;
        AR = false;
        SR = false;
    }

    public void setAR()
    {
        SG = false;
        AR = true;
        SR = false;
    }

    public void setSR()
    {
        SG = false;
        AR = false;
        SR = true;
    }

    public void setSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);
        Debug.Log(sfxSlider.value);
    }

    public void setMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicSlider.value);
        Debug.Log(musicSlider.value);
    }
}
