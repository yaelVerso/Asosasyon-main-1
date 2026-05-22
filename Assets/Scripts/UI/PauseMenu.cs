using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // Drag PausePanel here
    [SerializeField] private SettingsManager settingsManager; // Drag SettingsManager here
    private bool isPaused = false;
    public GameObject pauseButton; // Drag your pause button here

    private void Update()
    {
        // Only handle pause input if not in settings
        if (settingsManager == null || !settingsManager.IsSettingsOpen)
        {
            // Press Escape or P to pause/unpause
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        if (pauseButton != null)
            pauseButton.SetActive(false); // Hide pause button
        Time.timeScale = 0f; // Freeze game
        isPaused = true;

        // Show cursor for menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        if (pauseButton != null)
            pauseButton.SetActive(true); // Show pause button again
        Time.timeScale = 1f; // Unfreeze game
        isPaused = false;

        // Hide cursor back to game mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        if (settingsManager != null)
        {
            // Optional: Hide pause panel while in settings
            pausePanel.SetActive(false);
            settingsManager.OpenSettings();
        }
    }

    public void MainMenu()
    {

        Time.timeScale = 1f; // Unfreeze before loading menu
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}