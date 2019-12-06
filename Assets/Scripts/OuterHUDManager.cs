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

    void Start()
    {
        TitanMeterValue                = 0f;
        TitanMeterBar.fillAmount       = TitanMeterValue;
        FullyChargedTitanMeter.enabled = false;
        TitanIsReady                   = false;
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
}
