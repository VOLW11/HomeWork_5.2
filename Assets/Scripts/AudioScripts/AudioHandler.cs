using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private const float OffVolume = -80f;
    private const float OnVolume = 0f;

    private const string MusicKey = "MusicVolume";
    private const string SoundsKey = "SoundsVolume";

    private AudioMixerGroup _masterGroup;

    private const int OnVolumeSaveKey = 1;
    private const int OffVolumeSaveKey = -1;

    public AudioHandler(AudioMixerGroup masterGroup)
    {
        _masterGroup = masterGroup;
    }

    public void Initialize()
    {
        int musicSaveKey = PlayerPrefs.GetInt(MusicKey);

        if (musicSaveKey == 0 || musicSaveKey == OnVolumeSaveKey)
            OnMusic();
        else
            OffMusic();

        int soundsSaveKey = PlayerPrefs.GetInt(SoundsKey);

        if (soundsSaveKey == 0 || soundsSaveKey == OnVolumeSaveKey)
            OnSounds();
        else
            OffSounds();
    }

    public bool IsMusicOn() => PlayerPrefs.GetInt(MusicKey) == OnVolumeSaveKey;

    public bool IsSoundsOn() => PlayerPrefs.GetInt(SoundsKey) == OnVolumeSaveKey;

    public void OffMusic()
    {
        _masterGroup.audioMixer.SetFloat(MusicKey, OffVolume);
        PlayerPrefs.SetInt(MusicKey, OffVolumeSaveKey);
    }

    public void OnMusic()
    {
        _masterGroup.audioMixer.SetFloat(MusicKey, OnVolume);
        PlayerPrefs.SetInt(MusicKey, OnVolumeSaveKey);
    }

    public void OffSounds()
    {
        _masterGroup.audioMixer.SetFloat(SoundsKey, OffVolume);
        PlayerPrefs.SetInt(SoundsKey, OffVolumeSaveKey);
    }

    public void OnSounds()
    {
        _masterGroup.audioMixer.SetFloat(SoundsKey, OnVolume);
        PlayerPrefs.SetInt(SoundsKey, OnVolumeSaveKey);
    }
}
