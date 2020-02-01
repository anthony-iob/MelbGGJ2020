using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    // Update is called once per frame
    void Update()
    {
        score.text = "Time Survived: "  + GameManager.instance.GetElapsedMinutes().ToString() + "m" + GameManager.instance.GetElapsedSeconds() + "s";
    }
}
