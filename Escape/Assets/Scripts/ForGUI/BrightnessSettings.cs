using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider; // Слайдер для изменения яркости
    [SerializeField] private PostProcessVolume postProcessVolume; // Ссылка на PostProcessVolume
    private ColorGrading colorGrading; // Ссылка на эффект Color Grading

    private void Start()
    {
        // Получаем доступ к эффекту Color Grading из профиля PostProcessing
        postProcessVolume.profile.TryGetSettings(out colorGrading);

        // Загружаем сохраненное значение яркости
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        brightnessSlider.value = savedBrightness;
        SetBrightness(savedBrightness);

        // Подписываемся на изменения слайдера
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    private void SetBrightness(float value)
    {
        if (colorGrading != null)
        {
            // Изменяем значение Gain для регулировки яркости
            //colorGrading.basic.overrides = true;
            //colorGrading.basic.gain.value = Mathf.Lerp(1f, 3f, value); // Диапазон яркости
        }
    }

    public void SaveBrightnessSettings()
    {
        // Сохраняем значение яркости
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Отписываемся от событий
        brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
    }
}
