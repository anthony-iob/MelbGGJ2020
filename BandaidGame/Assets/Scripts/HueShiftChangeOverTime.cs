using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
[ExecuteInEditMode]
public class HueShiftChangeOverTime : MonoBehaviour
{
    public PostProcessProfile profile;
	public PostProcessProfile standard;

	private ColorGrading colorGradingSettings;

    public float tempChangeAmount = 2;
    public float tempChangeSpeed = 2;

    private void Awake()
    {
		standard.TryGetSettings(out colorGradingSettings);
		profile.TryGetSettings(out colorGradingSettings);
        colorGradingSettings.hueShift.value = 0;
    }

    void Update()
    {
        colorGradingSettings.hueShift.value = tempChangeAmount * Mathf.Sin(tempChangeSpeed * Time.time);
    }

    private void OnDestroy()
    {
        colorGradingSettings.hueShift.value = 0;
    }
}
