using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject settingsPanel;

    [Header("Controls")]
    public Slider musicSlider;  
    public Toggle musicToggle;  

    void Start()
    {
        // 1. Load dữ liệu từ máy (Mặc định volume 0.8, status là ON)
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        bool isOn = PlayerPrefs.GetInt("MusicStatus", 1) == 1;

        // 2. Cập nhật giao diện (Kiểm tra tránh lỗi Null)
        if (musicSlider != null) musicSlider.value = savedVolume;
        if (musicToggle != null) musicToggle.isOn = isOn;

        // 3. Áp dụng âm thanh ngay lập tức khi vào cảnh
        ApplyMusicSettings(savedVolume, isOn);
        
        Debug.Log($"[SettingsUI] Đã load: Volume {savedVolume}, Status {isOn}");
    }

    public void OnSliderChanged(float value)
    {
        if (musicToggle != null)
            ApplyMusicSettings(value, musicToggle.isOn);
    }

    public void OnToggleChanged(bool isOn)
    {
        if (musicSlider != null)
            ApplyMusicSettings(musicSlider.value, isOn);
    }

    private void ApplyMusicSettings(float volume, bool isOn)
    {
        float finalVolume = isOn ? volume : 0;
        
        // Điều khiển âm lượng toàn hệ thống
        AudioListener.volume = finalVolume;

        // Lưu dữ liệu vào máy
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetInt("MusicStatus", isOn ? 1 : 0);
        PlayerPrefs.Save();
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