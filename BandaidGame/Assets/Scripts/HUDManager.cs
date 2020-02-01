using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timer, percentage;

    void Update()
    {
        UpdateTimer();
        UpdatePercentage();
    }

    void UpdatePercentage()
    {
        percentage.text = (100 * GameManager.instance.GetFloodPercentage()).ToString("0.0") + "%";
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

        timer.text = text;
    }
}
