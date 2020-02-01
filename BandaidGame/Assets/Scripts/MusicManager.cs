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
    public AudioSource endLoop;


    public AudioClip introTrack;
    public AudioClip loop1Track;
    public AudioClip loop2Track;
    public AudioClip loop3Track;
    public AudioClip loop4Track;
    public AudioClip loop5Track;
    public AudioClip endLoopTrack;
    public AudioClip gameEndState;
    public AudioClip gameOver;

    public int trackNumber;
    public float fadeTime;
    private float currentTime;


    // private GameManager _gameManager;

    void Start()
    {
        // var gameController = GameObject.FindGameObjectWithTag("GameController");
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
        endLoop.volume = 0.0f;


    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
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


        }

    }
    private void FixedUpdate()
    {
        if (trackNumber == 1) { StartCoroutine("MusicLoop1"); }
        if (trackNumber == 2) { StartCoroutine("MusicLoop2"); }
        if (trackNumber == 3) { StartCoroutine("MusicLoop3"); }
        if (trackNumber == 3) { StartCoroutine("MusicLoop4"); }
        if (trackNumber == 3) { StartCoroutine("MusicLoop5"); }
    }

   IEnumerator MusicLoop1()
    {
       // loop1.volume = 1.0f;

        while (loop1.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop1.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop2.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
            yield return null;
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
    }


    IEnumerator MusicLoop3()
    {
        /* 
        loop1.volume = 0.0f;
        loop2.volume = 0.0f;
        loop3.volume = 1.0f;
        loop4.volume = 0.0f;
        loop5.volume = 0.0f;
        endLoop.volume = 0.0f;
        */
        while (loop2.volume > 0)
        {
            currentTime += Time.deltaTime;
            loop2.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
            loop3.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
            yield return null;
        }
    }

    IEnumerator MusicLoop4()
    {
        currentTime += Time.deltaTime;
        loop3.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
        loop4.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
        yield return null;
    }

    IEnumerator MusicLoop5()
    {
        currentTime += Time.deltaTime;
        loop4.volume = Mathf.Lerp(1, 0, currentTime / fadeTime);
        loop5.volume = Mathf.Lerp(0, 1, currentTime / fadeTime);
        yield return null;
    }

}