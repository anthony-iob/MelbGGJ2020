using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class updateScore : MonoBehaviour
{
    public TextMeshProUGUI timerLabel;
    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        int mins = GameManager.instance.GetElapsedMinutes();
        int secs = GameManager.instance.GetElapsedSeconds();
        int millisecs = GameManager.instance.GetElapsedMilliSeconds() % 1000;
		string text = "";

        if (mins >= 1)
        {
            text += mins + ":";
        }
        else
        {
            text += "0:";
        }

        if (secs >= 10)
        {
            text += secs + ":";
        }
        else
        {
            text += "0" + secs + ":";
        }

        if (millisecs >= 100)
        {
            text += millisecs;
        }
        else if (millisecs >= 10)
        {
            text += "0" + millisecs;
        }
        else if (millisecs == 1)
        {
            text += "00" + millisecs;
        }
        else
        {
            text += millisecs;
        }


        timerLabel.text = text;
    }
}
