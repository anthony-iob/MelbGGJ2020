using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource loop1;
    public AudioSource loop2;
    public AudioSource loop3;
    public AudioSource loop4;
    public AudioSource loop5;
    public AudioSource loop6;
    public AudioSource endLoop;


    public AudioClip introTrack;
    public AudioClip loop1Track;
    public AudioClip loop2Track;
    public AudioClip loop3Track;
    public AudioClip loop4Track;
    public AudioClip loop5Track;
    public AudioClip loop6Track;
    public AudioClip endLoopTrack;
    public AudioClip gameEndState;
    public AudioClip gameOver;

    public AudioClip klaxon;

    public int trackNumber;
    public float fadeTime;
    private float currentTime;

    /* This script involves creating audiosource children on a MusicManager gameobject, then dragging in the relevant tracks into the audioClip slots on the MusicManager script. 
     * When the functions are called it should fade between the current and next queued track in your list.
     * 
     * The intro track deos not need an object and will start populated in the first audiosource, when it stops playing it will be replaced by the first loop, and then subsequent loops 
     * will do the volume thing. 
     * 
     * There will be a system coming which swaps between two loop halves - allowing for different chord progressions to keep in time with each other - this will
     * be used primarily to trigger an upwards endstate track which climbs a scale and is illsuited to a fade in. 
     * 
     * Finally the gameEndState is very much the last thing which will be heard and should accompany the failure of this game - before playing the gameOver in time with the relevant text/state - 
     * that could also be added directly onto the GameOver text to be played although it would be better to have it here so we can try and avoid overlap. 
     */

    void Start()
    {

        
        // _gameManager = gameController.GetComponent<GameManager>();

        //turn off loop when threshold is passed - when not playing swap tracks. 

        loop1.clip = introTrack;
        loop1.Play();

        trackNumber = 0;

        loop1.volume = 1.0f;
        loop2.volume = 0.0f;
        loop3.volume = 0.0f;
        loop4.volume = 0.0f;
        loop5.volume = 0.0f;
        loop6.volume = 0.0f;
        endLoop.volume = 0.0f;


    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            //gameState health state system? 
            trackNumber += 1;
            Debug.Log(trackNumber);

        }


        if (!loop1.isPlaying)
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

            endLoop.clip = endLoopTrack;
            endLoop.Play();
            endLoop.loop = isActiveAndEnabled;
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.instance.GetFloodPercentage() >= 0.10) { StartCoroutine("MusicLoop1"); }
        if (GameManager.instance.GetFloodPercentage() >= 0.25) { StartCoroutine("MusicLoop2"); }
        if (GameManager.instance.GetFloodPercentage() >= 0.35) { StartCoroutine("MusicLoop3"); }
        if (GameManager.instance.GetFloodPercentage() >= 0.50) { StartCoroutine("MusicLoop4"); }
        if (GameManager.instance.GetFloodPercentage() >= 0.75)
        {
            StartCoroutine("MusicLoop5");
            //put in a klaxon/warning here? 
        }
        if (GameManager.instance.GetFloodPercentage() >= 85)
        {
            StartCoroutine("MusicLoop6");
            //warning here too?
        }

        if (GameManager.instance.GetFloodPercentage() >= 90)
        {
          //  endLoop.loop = !isActiveAndEnabled;
            endLoop.loop = false;
        }

        if (!endLoop.isPlaying && !loop1.isPlaying)
        {
            endLoop.clip = gameEndState;
            endLoop.Play();
        }
    }

   IEnumerator MusicLoop1()
    {

        while (loop1.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop1.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop2.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
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
            loop2.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop3.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
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
            loop3.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop4.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
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
            loop4.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop5.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
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
            loop5.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop6.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
            yield return null;
        }



    }

    IEnumerator MusicLoop6()
    {
        currentTime += Time.deltaTime;
        loop6.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
        endLoop.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
        yield return null;
    }

}