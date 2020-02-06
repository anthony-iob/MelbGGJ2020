﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class basicMenuLoop : MonoBehaviour

{

    public AudioSource audioSource;
    public AudioClip loopTrack;

    public AudioMixerSnapshot unpausedAudio;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        unpausedAudio.TransitionTo(0f);
        GameManager.instance.disablePewPew = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = loopTrack;
            audioSource.Play();
            audioSource.loop = isActiveAndEnabled;

        }
    }
}
