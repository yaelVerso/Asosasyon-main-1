using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SettingsManager settingsManager; // Add this line

    // Play button - loads your game scene
    public void PlayGame()
    {
        GameData.SavedHealth = -1; // Reset to default
        GameData.SavedDoggos = 0;
        Time.timeScale = 1f; // Make sure game isn't paused
        SceneManager.LoadScene("SampleScene"); // Change to your gameplay scene name
    }

    // Options button - opens settings
    public void OpenOptions()
    {
        // Open settings panel
        if (settingsManager != null)
            settingsManager.OpenSettings();
        else
            Debug.LogWarning("SettingsManager not assigned in MainMenu!");
    }

    // Quit button - exits the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}