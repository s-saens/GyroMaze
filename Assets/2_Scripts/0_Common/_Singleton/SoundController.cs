using UnityEngine;
using UnityEngine.Audio;

public class SoundController : SingletonMono<SoundController>
{
    [SerializeField] private AudioMixer mixer;


    private void OnEnable()
    {
        GameData.Settings.masterVolume.onChange += SetMasterVolume;

        GameData.Settings.masterVolume.value = PlayerPrefs.GetFloat(ConstData.KEY_MASTER_VOLUME, 0.7f);
    }
    private void OnDisable()
    {
        GameData.Settings.masterVolume.onChange -= SetMasterVolume;
    }

    private void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume == 0 ? -80 : Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(ConstData.KEY_MASTER_VOLUME, volume);
    }
}