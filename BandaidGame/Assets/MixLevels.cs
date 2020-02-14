using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{

    public AudioMixer masterMixer;



    private void Start()
    {
       //if (PlayerPrefs.GetFloat("MusicVolLevel") == 0)
        {
            PlayerPrefs.SetFloat("VoiceVolLevel", 0.8f);
            PlayerPrefs.SetFloat("MusicVolLevel", 0.8f);
            PlayerPrefs.SetFloat("SFXVolLevel", 0.8f);
        }

    }

    public void SetMusicLevel(float musicLevel)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log(musicLevel) * 20);
        PlayerPrefs.SetFloat("MusicVolLevel", musicLevel);
    }

    public void SetSFXLevel(float sfxLevel)
    {
        masterMixer.SetFloat("SFXVol", Mathf.Log(sfxLevel) * 20);
        PlayerPrefs.SetFloat("SFXVolLevel", sfxLevel);
    }

    public void SetVoicesLevel(float voiceLevel)
    {
        masterMixer.SetFloat("VoiceVol", Mathf.Log(voiceLevel) * 20);
        PlayerPrefs.SetFloat("VoiceVolLevel", voiceLevel);
    }

}