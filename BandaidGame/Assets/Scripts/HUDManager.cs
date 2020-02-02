using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerLabel, percentageLabel;
    public Image floodBar, chargeBar;
    float percentage;

    void Update()
    {
        UpdateTimer();
        UpdateFloodInfo();
        UpdateChargeBar();
    }

    void UpdateFloodInfo()
    {
        percentage = Mathf.Clamp(GameManager.instance.GetFloodPercentage(), 0f, 100f);
        percentageLabel.text = (100 * percentage).ToString("0");
        floodBar.fillAmount = percentage;
    }

    void UpdateTimer()
    {
        int mins = GameManager.instance.GetElapsedMinutes();
        int secs = GameManager.instance.GetElapsedSeconds();
        string text = "";

        if (mins > 0)
        {
            text += mins + "m";
        }

        text += GameManager.instance.GetElapsedSeconds() + "s";

        timerLabel.text = text;
    }

    void UpdateChargeBar()
    {
        chargeBar.fillAmount = Gun.instance.GetChargePercentage();
    }
}
