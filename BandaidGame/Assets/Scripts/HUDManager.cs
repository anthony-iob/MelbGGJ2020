using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerLabel, percentageLabel, percentageIcon;
    public Image floodBar, chargeBar;
    public GameObject percentageBox;
    public float percentageTextMin = 1.0f;
    public float percentageTextMax = 1.6f;
    float percentage;
    public Color emptyFillColour = Color.white;
    public Color fullFillColour = Color.green;

    void Update()
    {
        UpdateTimer();
        UpdateFloodInfo();
        UpdateChargeBar();
        UpdatePercentageScaler();
    }

    void UpdateFloodInfo()
    {
        percentage = Mathf.Clamp(GameManager.instance.GetFloodPercentage(), 0f, 100f);
        if (percentage <= 1f)
        {
            percentageLabel.text = (100 * percentage).ToString("0") + "%";
        }
        else
        {
            percentage = 1f;
            percentageLabel.text = (100 * percentage).ToString("0") + "%";
        }
        
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

    void UpdatePercentageScaler()
    {
        Vector3 percentageBoxScaleMin = new Vector3(percentageTextMin, percentageTextMin, percentageTextMin);
        Vector3 percentageBoxScaleMax = new Vector3(percentageTextMax, percentageTextMax, percentageTextMax);
        percentageLabel.color = Color.Lerp(emptyFillColour, fullFillColour, (percentage * 100) / 100f);
        percentageIcon.color = Color.Lerp(emptyFillColour, fullFillColour, (percentage * 100) / 100f);
        percentageBox.transform.localScale = Vector3.Lerp(percentageBoxScaleMin, percentageBoxScaleMax, (percentage * 100) / 100f);
    }
}
