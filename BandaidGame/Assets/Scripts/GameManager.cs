using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public int currentBloodLevel;
    public int MAX_BLOOD_LEVEL;
    public float timeElapsed;
    public UnityEvent gameOver;
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        currentBloodLevel = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isGameOver) {
            currentBloodLevel += NPCManager.instance.UpdateBloodLevel();
            timeElapsed += Time.deltaTime;
        }
    }

    void Update() {
        if(currentBloodLevel >= MAX_BLOOD_LEVEL) {
            gameOver.Invoke();
            isGameOver = true;
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
}
