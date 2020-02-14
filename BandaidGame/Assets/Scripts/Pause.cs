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
        pausedAudio.TransitionTo(.01f);
        Time.timeScale = 0;
        paused = true;
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        unpausedAudio.TransitionTo(.01f);
        paused = false;
        StartCoroutine(LetMeShoot());
    }

    IEnumerator LetMeShoot()
    {
        //Debug.Log("can't shoot yet");
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.disablePewPew = false;
        //Debug.Log("can shoot now");
    }
    
    public void InvokeResume() {
        resumeGame.Invoke();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 7")) {
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
