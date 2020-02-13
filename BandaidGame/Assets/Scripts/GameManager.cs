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
    //public Animator ChargeAnimator;
    public Animator UnderwaterAnimator;
    private bool invoked;
	public PostProcessProfile standard;
	public PostProcessProfile profile;
	private Vignette vig;
	private Vignette vig2;
	private LensDistortion lensD;
	private LensDistortion lensD2;
	private DepthOfField depth;
	private DepthOfField depth2;
	public GameObject floodLevel;
	public float effectFloodLevel = 0.27f;

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
		standard.TryGetSettings(out vig2);
		standard.TryGetSettings(out lensD2);
		standard.TryGetSettings(out depth2);
		profile.TryGetSettings(out vig);
		profile.TryGetSettings(out lensD);
		profile.TryGetSettings(out depth);

		vig.intensity.value = 0;
		vig2.intensity.value = 0;
		lensD.intensity.value = 0;
		lensD.scale.value = 1;
		lensD2.intensity.value = 0;
		lensD2.scale.value = 1;
		depth.focusDistance.value = 55;
		depth2.focusDistance.value = 55;
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
            //ChargeAnimator.SetBool("GameOver", true);
            
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

		if (floodLevel.transform.position.y >= effectFloodLevel)
		{
			//Debug.Log(floodLevel.transform.position.y);
			UnderwaterAnimator.SetBool("Underwater", true);
			vig.intensity.value = vigChangeAmount * Mathf.Sin(vigChangeSpeed * Time.unscaledTime);
			vig2.intensity.value = vigChangeAmount * Mathf.Sin(vigChangeSpeed * Time.unscaledTime);
			lensD.intensity.value = lensDChangeAmount * Mathf.Sin(lensDChangeSpeed * Time.unscaledTime) + 60;
			depth.focusDistance.value = 1;
			lensD2.intensity.value = lensDChangeAmount * Mathf.Sin(lensDChangeSpeed * Time.unscaledTime) + 60;
			depth2.focusDistance.value = 1;
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
