using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int currentBloodLevel;
    public int MAX_BLOOD_LEVEL;
    public float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        currentBloodLevel = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentBloodLevel += NPCManager.instance.UpdateBloodLevel();
    }

    void Update() {
        timeElapsed += Time.deltaTime;
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
