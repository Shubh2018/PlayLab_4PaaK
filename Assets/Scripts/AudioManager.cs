using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    [SerializeField] private AudioSource _musicAudioSource;
    
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _sfxAudioSourcePlayer1;
    [SerializeField] private AudioSource _sfxAudioSourcePlayer2;
    [SerializeField] private AudioSource _sfxAudioSourcePlayer3;
    [SerializeField] private AudioSource _sfxAudioSourcePlayer4;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private AudioClip _clickAudio;
    [SerializeField] private AudioClip _keyPress;
    [SerializeField] private AudioClip _captureAudio;
    [SerializeField] private AudioClip _hurtAudio;

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
    
    public void KeyPress()
    {
        if(!_sfxAudioSource.isPlaying)
            _sfxAudioSource.PlayOneShot(_keyPress);
    }

    public void Capture(InputManager.Player player)
    {
        AudioSource source;
        
        switch (player)
        {
            case InputManager.Player.Player1: source = _sfxAudioSourcePlayer1; break;
            case InputManager.Player.Player2: source = _sfxAudioSourcePlayer2; break;
            case InputManager.Player.Player3: source = _sfxAudioSourcePlayer3; break;
            case InputManager.Player.Player4: source = _sfxAudioSourcePlayer4; break;
            default: source = _sfxAudioSourcePlayer1; break;
        }
        
        if (!source.isPlaying)
            source.PlayOneShot(_captureAudio);
    }

    public void Hurt() 
    { 
        if (!_sfxAudioSourcePlayer1.isPlaying)
            _sfxAudioSourcePlayer1.PlayOneShot(_hurtAudio);
    }
        

    public void PlayMusic()
    {
        _musicAudioSource.clip = _musicClip;
        
        if(!_musicAudioSource.isPlaying)
            _musicAudioSource.Play();
    }
}
