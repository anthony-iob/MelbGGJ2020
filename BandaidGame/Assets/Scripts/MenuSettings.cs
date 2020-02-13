using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuSettings : MonoBehaviour
{
    public FirstPersonController playerCharacterController;
    public Slider sliderSensitivity;
    public Slider sliderVolume;
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
        sliderVolume.value = AudioListener.volume;
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
        
        AudioListener.volume = sliderVolume.value;
    }
}
