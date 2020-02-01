using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : Singleton<MonoBehaviour>
{
    public UnityEvent pauseGame, resumeGame;
    bool paused = false;
    // Start is called before the first frame update
    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        paused = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) ) {
            if(paused) {
                Debug.Log("resuming");
                resumeGame.Invoke();
            } else {
                Debug.Log("pausing");
                pauseGame.Invoke();
            }
        }
    }
}
