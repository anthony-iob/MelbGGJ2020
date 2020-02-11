using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : Singleton<GameManager>
{
    public float currentBloodLevel;
    public float MAX_BLOOD_LEVEL;
    public float timeElapsed;
    public UnityEvent gameOver;
    bool isGameOver = false;
    public bool disablePewPew = false;
    public Animator TimeAnimator;
    public Animator SlimeAnimator;
    public Animator ChargeAnimator;
    private bool invoked;
	public PostProcessProfile profile;
	private Vignette vig;
	private LensDistortion lensD;

	public float vigChangeAmount = 2;
	public float vigChangeSpeed = 2;
	public float lensDChangeAmount = 2;
	public float lensDChangeSpeed = 2;

	public AudioMixerSnapshot unpausedAudio;

    public GameObject gameOverHUD, HUD, pauseMenu, player;
  
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        currentBloodLevel = 0;
        Time.timeScale = 1;
        unpausedAudio.TransitionTo(0f);
        invoked = false;
		profile.TryGetSettings(out vig);
		profile.TryGetSettings(out lensD);
		vig.intensity.value = 0;
		lensD.intensity.value = 0;
		lensD.scale.value = 1;
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

    void Update()
    {
        if (currentBloodLevel >= MAX_BLOOD_LEVEL)
        {
            // gameOver.Invoke(); 
            // removed this to put in delayed game end below.


            isGameOver = true;
            TimeAnimator.SetBool("GameOver", true);
            SlimeAnimator.SetBool("GameOver", true);
            ChargeAnimator.SetBool("GameOver", true);
            //Time.timeScale = 0;
            //HUD.SetActive(false);
            pauseMenu.SetActive(false);

			if (!MusicManager.instance.endLoop.isPlaying)
            {
                gameOverHUD.SetActive(true);
                Time.timeScale = 0;
                disablePewPew = true;

                if (!invoked)
                {
                    gameOver.Invoke();
                    invoked = true;
                }          

                // if (player != null) player.SetActive(false); else Debug.Log("Attach the player in gamemanager plz");

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;


            }
        }

		if (currentBloodLevel >= 10000)
		{
			vig.intensity.value = vigChangeAmount * Mathf.Sin(vigChangeSpeed * Time.unscaledTime);
			lensD.intensity.value = lensDChangeAmount * Mathf.Sin(lensDChangeSpeed * Time.unscaledTime) + 60;
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
