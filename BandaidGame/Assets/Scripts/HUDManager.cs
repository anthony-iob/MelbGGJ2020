using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerLabel, percentageLabel, percentageIcon;
    public Image floodBar, chargeBar;
    float percentage;
    public Color emptyFillColour = Color.white;
    public Color fullFillColour = Color.green;

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
        percentageLabel.color = Color.Lerp(emptyFillColour, fullFillColour, (percentage * 100) / 100f);
        percentageIcon.color = Color.Lerp(emptyFillColour, fullFillColour, (percentage * 100) / 100f);
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
