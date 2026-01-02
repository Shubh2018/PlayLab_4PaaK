using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip _clickAudio;
    [SerializeField] private AudioClip _captureAudio;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void Click()
    {
        if(!_sfxAudioSource.isPlaying)
            _sfxAudioSource.PlayOneShot(_clickAudio);
    }

    public void Capture()
    {
        if (!_sfxAudioSource.isPlaying)
            _sfxAudioSource.PlayOneShot(_captureAudio);
    }
}
