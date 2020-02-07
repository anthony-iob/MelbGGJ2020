using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOver : MonoBehaviour
{
	public PostProcessProfile profile;
	private DepthOfField depthOfFieldSettings;
	public FirstPersonController fpsScript;

	private void Awake()
	{
		profile.TryGetSettings(out depthOfFieldSettings);
		depthOfFieldSettings.focusDistance.value = 55;
		depthOfFieldSettings.focalLength.value = 80;
		depthOfFieldSettings.aperture.value = 50;
	}

	public void CursorShow()
    {
		fpsScript.m_MouseLook.SetCursorLock(false);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void FocusDistance()
	{
		depthOfFieldSettings.focusDistance.value = 1;
		depthOfFieldSettings.focalLength.value = 300;
		depthOfFieldSettings.aperture.value = 1;
	}
}
