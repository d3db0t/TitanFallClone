using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OuterHUDManager : MonoBehaviour
{
    public Image FullyChargedTitanMeter;
    public Image TitanMeterBar;
    public float TitanMeterValue;
    public bool TitanIsReady;
    public float CurrentHP;
    public float MaxHP;
    public Image HealthBar;
    public Text HealthBarValue;
    public float NextTimeToGenerateHealth;

    void Start()
    {
        TitanMeterValue                = 0f;
        TitanMeterBar.fillAmount       = TitanMeterValue;
        FullyChargedTitanMeter.enabled = false;
        TitanIsReady                   = false;
        CurrentHP                      = MaxHP;
        HealthBarValue.text            = MaxHP.ToString();
    }

    void Update()
    {
        if (Time.time >= NextTimeToGenerateHealth && CurrentHP < MaxHP)
        {
            CurrentHP += 0.05f;
            HealthBar.fillAmount = CurrentHP / MaxHP;
            HealthBarValue.text  = ((int) CurrentHP).ToString();
        }
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
        // GameOverCanvas
        // TODO
    }
}
