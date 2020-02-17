using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //this is important if you wanna use snapshots - they're controlled in the pause menu but also here for volume increases. 

public class MusicManager : Singleton<MusicManager>
{
    /* This script involves creating audiosource children on a MusicManager gameobject, then dragging in the relevant tracks into the audioClip slots on the MusicManager script. 
 * When the functions are called it should fade between the current and next queued track in your list. 
 * 
 * The intro track deos not need an object and will start populated in the first audiosource, when it stops playing it will be replaced by the first loop, and then subsequent loops 
 * will do the volume thing. 
 * 
 * There was going to be a system coming which swaps between two loop halves - allowing for different chord progressions to keep in time with each other - this will
 * be used primarily to trigger an upwards endstate track which climbs a scale and is illsuited to a fade in. This is done by turning off the loop on the last track of the first half, 
 * and when that track finishes the next track in the system plays which could be part of a separate loop.
 * 
 * Finally the gameEndState is very much the last thing which will be heard and should accompany the failure of this game - before playing the gameOver in time with the relevant text/state - 
 * that could also be added directly onto the GameOver text to be played although it would be better to have it here so we can try and avoid overlap - that would depend on Game Over 
 * implentation technique.
 * 
 * Any questions ask that Jer guy he knows what's up (not really I made so many mistakes please don't judge me). 
 */


    [Header("AudioSources")]
    public AudioSource loop1;
    public AudioSource loop2;
    public AudioSource loop3;
    public AudioSource loop4;
    public AudioSource loop5;
    public AudioSource loop6;
    public AudioSource loop7;
    public AudioSource endLoop;

    //public AudioSource klaxonAudioSource;

    [Header("AudioClips")]
    public AudioClip introTrack;
    public AudioClip loop1Track;
    public AudioClip loop2Track;
    public AudioClip loop3Track;
    public AudioClip loop4Track;
    public AudioClip loop5Track;
    public AudioClip loop6Track;
    public AudioClip loop7Track;
    public AudioClip endLoopTrack;
    public AudioClip gameEndState;
    public AudioClip gameOver;
    public AudioClip cPedal;

    //public AudioClip klaxon;

   // public int trackNumber;
   [Header("Time to fade between tracks (ms)")]
    public float musicFadeTime;
    private float currentTime;
    public float endLoopPercentage = 0.85f;

    private bool ohNoEndTime = false;



    void Start()
    {

        
        // _gameManager = gameController.GetComponent<GameManager>();

        //turn off loop when threshold is passed - when not playing swap tracks. 

        loop1.clip = introTrack;
        loop1.Play();

       // trackNumber = 0; //btw this is useless I should delete it but here I am writing a comment instead

        loop1.volume = 1.0f;
        loop2.volume = 0.0f;
        loop3.volume = 0.0f;
        loop4.volume = 0.0f;
        loop5.volume = 0.0f;
        loop6.volume = 0.0f;
        loop7.volume = 0.0f;
        endLoop.volume = 0.0f;


    }

    void Update()
    {
     
        if (!loop1.isPlaying && endLoop != gameEndState)
        {
            loop1.clip = loop1Track;
            loop1.Play();
            loop1.loop = isActiveAndEnabled;
		
            loop2.clip = loop2Track;
            loop2.Play();
            loop2.loop = isActiveAndEnabled;

            loop3.clip = loop3Track;
            loop3.Play();
            loop3.loop = isActiveAndEnabled;

            loop4.clip = loop4Track;
            loop4.Play();
            loop4.loop = isActiveAndEnabled;

            loop5.clip = loop5Track;
            loop5.Play();
            loop5.loop = isActiveAndEnabled;

            loop6.clip = loop6Track;
            loop6.Play();
            loop6.loop = isActiveAndEnabled;

            loop7.clip = loop7Track;
            loop7.Play();
            loop7.loop = isActiveAndEnabled;

            endLoop.clip = endLoopTrack;
            endLoop.Play();
            endLoop.loop = isActiveAndEnabled;
        }

   // }
  //  private void FixedUpdate()
 //   {
        if (loop1.clip == loop1Track)
        {
            if (GameManager.instance.GetFloodPercentage() >= 0.02 && GameManager.instance.GetFloodPercentage() < 0.08) { StartCoroutine("MusicLoop1"); }
            if (GameManager.instance.GetFloodPercentage() >= 0.08 && GameManager.instance.GetFloodPercentage() < 0.20) { StartCoroutine("MusicLoop2"); }
            if (GameManager.instance.GetFloodPercentage() >= 0.20 && GameManager.instance.GetFloodPercentage() < 0.30) { StartCoroutine("MusicLoop3"); }
            if (GameManager.instance.GetFloodPercentage() >= 0.35 && GameManager.instance.GetFloodPercentage() < 0.45) { StartCoroutine("MusicLoop4"); }
            if (GameManager.instance.GetFloodPercentage() >= 0.50 && GameManager.instance.GetFloodPercentage() < 0.60) { StartCoroutine("MusicLoop5"); }
            if (GameManager.instance.GetFloodPercentage() >= 0.65 && GameManager.instance.GetFloodPercentage() < 0.65) { StartCoroutine("MusicLoop6"); }

            if (GameManager.instance.GetFloodPercentage() >= 0.70 && GameManager.instance.GetFloodPercentage() < 0.75)
            {
                StartCoroutine("MusicLoop7");

                //alarm again?
                //klaxonAudioSource.Play();
            }

            if (GameManager.instance.GetFloodPercentage() >= 0.81)
            {
                //endLoop.loop = false;
                //Debug.Log("Loop should have turned off now!! End state approacheth");
            }

            //if (!endLoop.isPlaying && GameManager.instance.GetFloodPercentage() >= 0.85 && ohNoEndTime == false)
            if (GameManager.instance.GetFloodPercentage() >= endLoopPercentage && ohNoEndTime == false)
            {
                loop1.volume = 0;
                loop2.volume = 0;
                loop3.volume = 0;
                loop4.volume = 0;
                loop5.volume = 0;
                loop6.volume = 0;


                loop7.clip = cPedal;
                loop7.volume = 100;
                loop7.loop = false;
                loop7.Play();
                endLoop.clip = gameEndState;
                endLoop.volume = 100;
                endLoop.Play();
                endLoop.loop = false;
                //Debug.Log("...last trak");
               // trackNumber = 8;
                ohNoEndTime = true;
               // currentTime = 0;

            }
        }
     }

   IEnumerator MusicLoop1()
    {

        while (loop1.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop1.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop2.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
            //Debug.Log("Track 2 is now playing");
            //trackNumber = 2;
            yield return null;
        }

        if (loop1.volume == 0)
        {
            currentTime = 0;
        }

    }
    
    IEnumerator MusicLoop2()
    {
        while (loop2.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop2.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop3.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
            //Debug.Log("Track 3 is now playing");

            loop1.volume = 0;

           // trackNumber = 3;
            yield return null;
        }

        if (loop2.volume == 0)
        {
            currentTime = 0;
        }
    }


    IEnumerator MusicLoop3()
    {
 
        while (loop3.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop3.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop4.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
            //Debug.Log("Track 4 is now playing");

            loop1.volume = 0;

            // trackNumber = 4;
            yield return null;
        }

        if (loop3.volume == 0)
        {
            currentTime = 0;
        }
    }

    IEnumerator MusicLoop4()
    {
        while (loop4.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop4.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop5.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
           // Debug.Log("Track 5 is now playing");

            loop3.volume = 0;

            // trackNumber = 5;
            yield return null;
        }
        if (loop4.volume == 0)
        {
            currentTime = 0;
        }

    }

    IEnumerator MusicLoop5()
    {
        while (loop5.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop5.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop6.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);

            loop4.volume = 0;
            
            //Debug.Log("Track 6 is now playing");
           // trackNumber = 6;
            yield return null;
        }
        if (loop5.volume == 0)
        {
            currentTime = 0;
        }
     }

    IEnumerator MusicLoop6()
    {
        while (loop6.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop6.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
            loop7.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
            //Debug.Log("Track 7 is now playing");

            loop5.volume = 0;

            // trackNumber = 7;
            yield return null;
        }
    }

    IEnumerator MusicLoop7()
    {
        currentTime += Time.deltaTime;
        loop7.volume = Mathf.Lerp(1, 0, currentTime / musicFadeTime);
        endLoop.volume = Mathf.Lerp(0, 1, currentTime / musicFadeTime);
        //Debug.Log("The End Loop is now playing");

        loop6.volume = 0;
        yield return null;
    }

}