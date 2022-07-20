using UnityEngine;
using UnityEngine.UI;

public class SoundSliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        slider.value = GameData.Settings.masterVolume.value;
        slider.onValueChanged.AddListener(SetVolume);
        GameData.Settings.masterVolume.onChange += SetSlider;
    }

    private void SetVolume(float volume)
    {
        GameData.Settings.masterVolume.value = volume;
    }

    private void SetSlider(float volume)
    {
        slider.onValueChanged.RemoveListener(SetVolume);
        slider.value = volume;
        slider.onValueChanged.AddListener(SetVolume);
    }
}