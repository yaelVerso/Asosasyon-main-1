using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    public void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        // Play game over sound
        if (SoundManager.instance != null && gameOverSound != null)
            SoundManager.instance.PlaySound(gameOverSound);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable pause while dead (so you can't unpause)
        PauseMenu pauseMenu = FindAnyObjectByType<PauseMenu>();
        if (pauseMenu != null)
            pauseMenu.enabled = false;
    }

    public void Respawn()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Re-enable pause menu for next time
        PauseMenu pauseMenu = FindAnyObjectByType<PauseMenu>();
        if (pauseMenu != null)
            pauseMenu.enabled = true;

        PlayerRespawn playerRespawn = FindAnyObjectByType<PlayerRespawn>();
        if (playerRespawn != null)
            playerRespawn.CheckRespawn();
    }

    public void MainMenu()
    {

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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