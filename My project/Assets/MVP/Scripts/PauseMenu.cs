using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Dit script zit op de pauseMenu parent

    public void LoadMainMenu()
    {
        Debug.Log("Try to load main menu");

        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.UnPauseAllAudio();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void OpenPauseMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;

        AudioManager.Instance.PauseAllAudio();
    }

    public void ClosePauseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;

        AudioManager.Instance.UnPauseAllAudio();
    }
}
