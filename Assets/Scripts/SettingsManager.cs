using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject pausePanel; 


    public bool IsSettingsOpen => settingsPanel != null && settingsPanel.activeSelf;
    private void Start()
    {
        // Start hidden
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Load current volumes into sliders
        if (SoundManager.instance != null)
        {
            musicSlider.value = SoundManager.instance.GetMusicVolume();
            sfxSlider.value = SoundManager.instance.GetSFXVolume();
            ambienceSlider.value = SoundManager.instance.GetAmbienceVolume();
        }

        // Add listeners
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        ambienceSlider.onValueChanged.AddListener(OnAmbienceVolumeChanged);
        backButton.onClick.AddListener(CloseSettings);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        // Hide pause panel while in settings
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Refresh slider values
        if (SoundManager.instance != null)
        {
            musicSlider.value = SoundManager.instance.GetMusicVolume();
            sfxSlider.value = SoundManager.instance.GetSFXVolume();
            ambienceSlider.value = SoundManager.instance.GetAmbienceVolume();
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        // Show pause panel again
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetMusicVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetSFXVolume(value);
    }
    private void OnAmbienceVolumeChanged(float value) 
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetAmbienceVolume(value);
    }
}