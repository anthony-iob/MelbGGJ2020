using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Audio;
#if Unity_Editor
using UnityEditor;
#endif

public class MenuSettings : Singleton<MenuSettings>
{
    public FirstPersonController playerCharacterController;
    public Slider sliderSensitivity;
    public Slider sliderMusic;
	public Slider sliderSounds;
	public Slider sliderVoices;
	public GameObject pauseMenu;


	void Start()
    {
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		if (playerCharacterController != null)
        {
            if (playerCharacterController.m_MouseLook != null)
            {
                if (sliderSensitivity != null)
                {
                    sliderSensitivity.value = playerCharacterController.m_MouseLook.XSensitivity;
                    sliderSensitivity.value = playerCharacterController.m_MouseLook.YSensitivity;
                }
            }
        }
       
        AudioListener.volume = 1.0f;
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolLevel");
        sliderSounds.value = PlayerPrefs.GetFloat("SFXVolLevel");
        sliderVoices.value = PlayerPrefs.GetFloat("VoiceVolLevel");


    }

    void Update()
    {
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		if (playerCharacterController != null)
        {
            if (playerCharacterController.m_MouseLook != null)
            {
                playerCharacterController.m_MouseLook.XSensitivity = sliderSensitivity.value;
                playerCharacterController.m_MouseLook.YSensitivity = sliderSensitivity.value;
            }
        }

    }


}
