using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioExample : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixerGroup);
    }

    private void Start()
    {
        _audioHandler.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _audioHandler.OnMusic();

        if (Input.GetKeyDown(KeyCode.S))
            _audioHandler.OffMusic();

        if (Input.GetKeyDown(KeyCode.D))
            _audioHandler.OnSounds();

        if (Input.GetKeyDown(KeyCode.F))
            _audioHandler.OffSounds();
    }
}
