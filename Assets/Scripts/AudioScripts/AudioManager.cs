using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixerGroup);
    }

    private void Start()
    {
        _audioHandler.Initialize();
    }

    public void OnButtonClickMusic()
    {
        if (_audioHandler.IsMusicOn())
            _audioHandler.OffMusic();
        else
            _audioHandler.OnMusic();
    }

    public void OnButtonClickSounds()
    {

        if (_audioHandler.IsSoundsOn())
            _audioHandler.OffSounds();
        else
            _audioHandler.OnSounds();
    }

    public void OnBombExplosion()
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
