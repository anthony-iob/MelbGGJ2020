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
		string text = "";

        if (mins > 0)
        {
            text += mins + "m";
        }

		text += GameManager.instance.GetElapsedSeconds() + "s";

		timerLabel.text = text;
    }
}
