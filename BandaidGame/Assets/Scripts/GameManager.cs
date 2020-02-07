using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    public float currentBloodLevel;
    public float MAX_BLOOD_LEVEL;
    public float timeElapsed;
    public UnityEvent gameOver;
    bool isGameOver = false;
    public bool disablePewPew = false;

    public AudioMixerSnapshot unpausedAudio;

    public GameObject gameOverHUD, HUD, pauseMenu;
	public bool calledGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        currentBloodLevel = 0;
        Time.timeScale = 1;
        unpausedAudio.TransitionTo(0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameOver == false)
        {
            currentBloodLevel += NPCManager.instance.UpdateBloodLevel();
            timeElapsed += Time.deltaTime;
        }
    }

    void Update() {
        if(currentBloodLevel >= MAX_BLOOD_LEVEL) {
            // gameOver.Invoke(); 
            // removed this to put in delayed game end below.


            isGameOver = true;
            //Time.timeScale = 0;
            HUD.SetActive(false);
            pauseMenu.SetActive(false);

            if (!MusicManager.instance.endLoop.isPlaying)
            {
				if (!calledGameOver)
				{
					gameOver.Invoke();
					calledGameOver = true;
				}

                gameOverHUD.SetActive(true);
                Time.timeScale = 0;
                disablePewPew = true; //this turns off player shoot controls. 
            }

        }
    }

    public int GetElapsedMilliSeconds() {
        return (int)(timeElapsed * 100);
    }

    public int GetElapsedSeconds() {
        return (int)(timeElapsed % 60);
    }

    public int GetElapsedMinutes() {
        return (int)(timeElapsed / 60);
    }

    public float GetFloodPercentage() {
        return currentBloodLevel / MAX_BLOOD_LEVEL;
    }
}
