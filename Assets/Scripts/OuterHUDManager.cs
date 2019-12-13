using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class OuterHUDManager : MonoBehaviour
{
    public Image FullyChargedTitanMeter;
    public Image TitanMeterBar;
    public float TitanMeterValue;
    public bool TitanIsReady;
    public bool TitanSpawned;
    public float CurrentHP;
    public float MaxHP;
    public Image HealthBar;
    public Text HealthBarValue;
    public float NextTimeToGenerateHealth;
    public GameObject gameoverScreen;
    public GameObject gameoverScreen;
    public GameObject Player;
    public GameObject PlayerTitan;
    public GameObject PlayerTitan3DModel;

    void Start()
    {
        TitanMeterValue                = 0f;
        TitanMeterBar.fillAmount       = TitanMeterValue;
        FullyChargedTitanMeter.enabled = false;
        TitanIsReady                   = false;
        CurrentHP                      = MaxHP;
        HealthBarValue.text            = MaxHP.ToString();
        TitanSpawned                   = false;
    }

    void Update()
    {
        if (Time.time >= NextTimeToGenerateHealth && CurrentHP < MaxHP)
        {
            CurrentHP += 0.05f;
            HealthBar.fillAmount = CurrentHP / MaxHP;
            HealthBarValue.text  = ((int) CurrentHP).ToString();
        }

        if (TitanIsReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CallTitan();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.GetComponent<RigidbodyFirstPersonController>().PlayerInTitanRange)
            {
                Player.SetActive(false);
                PlayerTitan.SetActive(true);
                PlayerTitan3DModel.SetActive(false);
            }
        }
    }

    public void CallTitan()
    {
        PlayerTitan3DModel.GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x + 2, Player.GetComponent<Transform>().position.y + 2, Player.GetComponent<Transform>().position.z + 2);
        PlayerTitan3DModel.SetActive(true);
        TitanIsReady = false;
        TitanMeterValue = 0f;
        FullyChargedTitanMeter.enabled = false;
        TitanSpawned = true;
        PlayerTitan.GetComponent<Transform>().position = PlayerTitan3DModel.GetComponent<Transform>().position;
    }

    public void IncreaseTitanMeter(float value)
    {
        TitanMeterValue += value;
        TitanMeterBar.fillAmount = TitanMeterValue / 100f;
        if (TitanMeterValue >= 100f)
        {
            TitanIsReady = true;
            FullyChargedTitanMeter.enabled = true;
        }
    }

    public void TakeDamage(float dmg)
    {
        CurrentHP -= dmg;
        HealthBar.fillAmount = CurrentHP / MaxHP;
        HealthBarValue.text  = ((int) CurrentHP).ToString();
        NextTimeToGenerateHealth = Time.time + 3f; // 3 seconds
        Debug.Log(CurrentHP);
        if (CurrentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameoverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
