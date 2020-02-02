using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMenuLoop : MonoBehaviour

{

    public AudioSource audioSource;
    public AudioClip loopTrack;

    // Start is called before the first frame update
    void Start()
    {
        
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
