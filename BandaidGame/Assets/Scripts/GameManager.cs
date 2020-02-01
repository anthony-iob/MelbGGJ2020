using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public float currentBloodLevel;
    public float MAX_BLOOD_LEVEL;
    public float timeElapsed;
    public UnityEvent gameOver;
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        currentBloodLevel = 0;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentBloodLevel += NPCManager.instance.UpdateBloodLevel();
        timeElapsed += Time.deltaTime;
    }

    void Update() {
        if(currentBloodLevel >= MAX_BLOOD_LEVEL) {
            gameOver.Invoke();
            isGameOver = true;
            Time.timeScale = 0;
        }
    }

    public int GetElapsedMilliSeconds() {
        return (int)Mathf.Round(timeElapsed * 100);
    }

    public int GetElapsedSeconds() {
        return (int)Mathf.Round(timeElapsed);
    }

    public int GetElapsedMinutes() {
        return (int)Mathf.Round(timeElapsed / 60);
    }

    public float GetFloodPercentage() {
        return currentBloodLevel / MAX_BLOOD_LEVEL;
    }
}
