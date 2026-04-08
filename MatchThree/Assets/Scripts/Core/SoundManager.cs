using UnityEngine;
using Tools;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [Header("SFX")]
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioClip _matchRemovedClip;
    [SerializeField] private AudioSource _audioSource2;
    private AudioSource _audioSource;

    [Header("Music")]
    [SerializeField] private AudioSource _musicSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();

        Debug.Log("[SoundManager] Awake");
        Debug.Log("[SoundManager] _audioSource = " + (_audioSource != null));
        Debug.Log("[SoundManager] _audioSource2 = " + (_audioSource2 != null));
        Debug.Log("[SoundManager] _musicSource = " + (_musicSource != null));

        LoadSettings();
    }

    private void LoadSettings()
    {
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        bool sfxOn = PlayerPrefs.GetInt("SFXStatus", 1) == 1;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        bool musicOn = PlayerPrefs.GetInt("MusicStatus", 1) == 1;

        Debug.Log("[SoundManager] LoadSettings SFX=" + sfxVolume + " / " + sfxOn);
        Debug.Log("[SoundManager] LoadSettings Music=" + musicVolume + " / " + musicOn);

        ApplySFXSettings(sfxVolume, sfxOn);
        ApplyMusicSettings(musicVolume, musicOn);
    }

    public void PlaySound(int index)
    {
        bool sfxOn = PlayerPrefs.GetInt("SFXStatus", 1) == 1;
        if (!sfxOn) return;

        if (_audioSource != null && index >= 0 && index < _audioClips.Length)
        {
            _audioSource.PlayOneShot(_audioClips[index]);
        }
    }

    public void PlayRandomInRangeOf(int inclusiveIndex, int exclusiveIndex)
    {
        bool sfxOn = PlayerPrefs.GetInt("SFXStatus", 1) == 1;
        if (!sfxOn) return;

        if (_audioSource != null && _audioClips.Length > 0)
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(inclusiveIndex, exclusiveIndex)]);
        }
    }

    public void PlayMatchRemovedClip()
    {
        bool sfxOn = PlayerPrefs.GetInt("SFXStatus", 1) == 1;
        if (!sfxOn) return;

        if (_audioSource2 != null && _matchRemovedClip != null)
        {
            _audioSource2.PlayOneShot(_matchRemovedClip);
        }
    }

    public void ApplySFXSettings(float volume, bool isOn)
    {
        float finalVolume = isOn ? volume : 0f;

        Debug.Log("[SoundManager] ApplySFXSettings volume=" + volume + " isOn=" + isOn);

        if (_audioSource != null)
            _audioSource.volume = finalVolume;

        if (_audioSource2 != null)
            _audioSource2.volume = finalVolume;

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetInt("SFXStatus", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ApplyMusicSettings(float volume, bool isOn)
    {
        Debug.Log("[SoundManager] ApplyMusicSettings volume=" + volume + " isOn=" + isOn);

        if (_musicSource == null)
        {
            Debug.LogError("[SoundManager] _musicSource is NULL");
            return;
        }

        _musicSource.volume = volume;

        if (isOn)
        {
            if (!_musicSource.isPlaying)
                _musicSource.Play();
            else
                _musicSource.UnPause();
        }
        else
        {
            _musicSource.Pause();
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetInt("MusicStatus", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 0.8f);
    }

    public bool GetSFXStatus()
    {
        return PlayerPrefs.GetInt("SFXStatus", 1) == 1;
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 0.8f);
    }

    public bool GetMusicStatus()
    {
        return PlayerPrefs.GetInt("MusicStatus", 1) == 1;
    }
}