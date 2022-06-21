using UnityEngine;
using UnityEngine.UI;

public class SoundSliderController : MonoBehaviour
{
    [SerializeField] private SettingsData settingsData;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(SetVolume);
        settingsData.masterVolume.onChange += SetSlider;
    }

    private void SetVolume(float volume)
    {
        settingsData.masterVolume.value = volume;
    }

    private void SetSlider(float volume)
    {
        slider.onValueChanged.RemoveListener(SetVolume);
        slider.value = volume;
        slider.onValueChanged.AddListener(SetVolume);
    }
}