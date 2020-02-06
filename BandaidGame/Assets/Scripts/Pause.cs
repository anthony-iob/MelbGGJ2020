using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class Pause : Singleton<MonoBehaviour>
{
    public UnityEvent pauseGame, resumeGame;
    bool paused = false;

    public AudioMixerSnapshot pausedAudio;
    public AudioMixerSnapshot unpausedAudio;

    // Start is called before the first frame update
    public void PauseGame()
    {
        GameManager.instance.disablePewPew = true;
        pausedAudio.TransitionTo(0f);
        Time.timeScale = 0;
        paused = true;
        
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        unpausedAudio.TransitionTo(0f);
        paused = false;
        GameManager.instance.disablePewPew = false;
    }
    
    public void InvokeResume() {
        resumeGame.Invoke();
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
