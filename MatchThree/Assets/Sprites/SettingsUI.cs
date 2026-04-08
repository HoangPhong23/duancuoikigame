using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject settingsPanel;

    [Header("SFX Controls")]
    public Slider sfxSlider;
    public Toggle sfxToggle;

    [Header("Music Controls")]
    public Slider musicSlider;
    public Toggle musicToggle;

    void Start()
    {
        Debug.Log("[SettingsUI] Start");

        if (SoundManager.Instance == null)
        {
            Debug.LogError("[SettingsUI] SoundManager.Instance is NULL");
            return;
        }

        if (sfxSlider != null) sfxSlider.value = SoundManager.Instance.GetSFXVolume();
        if (sfxToggle != null) sfxToggle.isOn = SoundManager.Instance.GetSFXStatus();

        if (musicSlider != null) musicSlider.value = SoundManager.Instance.GetMusicVolume();
        if (musicToggle != null) musicToggle.isOn = SoundManager.Instance.GetMusicStatus();

        SoundManager.Instance.ApplySFXSettings(sfxSlider.value, sfxToggle.isOn);
        SoundManager.Instance.ApplyMusicSettings(musicSlider.value, musicToggle.isOn);

        Debug.Log("[SettingsUI] Loaded settings");
    }

    public void OnSFXSliderChanged(float value)
    {
        Debug.Log("[SettingsUI] OnSFXSliderChanged: " + value);

        if (SoundManager.Instance != null && sfxToggle != null)
        {
            SoundManager.Instance.ApplySFXSettings(value, sfxToggle.isOn);
        }
    }

    public void OnSFXToggleChanged(bool isOn)
    {
        Debug.Log("[SettingsUI] OnSFXToggleChanged: " + isOn);

        if (SoundManager.Instance != null && sfxSlider != null)
        {
            SoundManager.Instance.ApplySFXSettings(sfxSlider.value, isOn);
        }
    }

    public void OnMusicSliderChanged(float value)
    {
        Debug.Log("[SettingsUI] OnMusicSliderChanged: " + value);

        if (SoundManager.Instance != null && musicToggle != null)
        {
            SoundManager.Instance.ApplyMusicSettings(value, musicToggle.isOn);
        }
    }

    public void OnMusicToggleChanged(bool isOn)
    {
        Debug.Log("[SettingsUI] OnMusicToggleChanged: " + isOn);

        if (SoundManager.Instance != null && musicSlider != null)
        {
            SoundManager.Instance.ApplyMusicSettings(musicSlider.value, isOn);
        }
    }

    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }
}