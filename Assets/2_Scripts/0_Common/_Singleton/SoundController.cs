using UnityEngine;
using UnityEngine.Audio;

public class SoundController : SingletonMono<SoundController>
{
    [SerializeField] private SettingsData settings;
    [SerializeField] private AudioMixer mixer;


    private void OnEnable()
    {
        settings.masterVolume.onChange += SetMasterVolume;

        settings.masterVolume.value = PlayerPrefs.GetFloat(ConstData.KEY_MASTER_VOLUME, 0);
    }
    private void OnDisable()
    {
        settings.masterVolume.onChange -= SetMasterVolume;
    }

    private void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
    }
}