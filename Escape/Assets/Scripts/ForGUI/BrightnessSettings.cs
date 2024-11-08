using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider; 
    [SerializeField] private PostProcessVolume postProcessVolume; 
    private ColorGrading colorGrading; 

    private void Start()
    {
        
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
           
            float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
            brightnessSlider.value = savedBrightness;
            SetBrightness(savedBrightness);

            
            brightnessSlider.onValueChanged.AddListener(SetBrightness);
        }
    }

    private void SetBrightness(float value)
    {
        if (colorGrading != null)
        {
            
            colorGrading.enabled.value = true;
            colorGrading.gain.overrideState = true;
            colorGrading.gain.value = new Vector4(value, value, value, 0); 
        }
    }

    public void SaveBrightnessSettings()
    {
        
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        
        brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
    }
}
