using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip titleMusic;

    [SerializeField]
    private AudioClip loadLevelSound;

    [SerializeField]
    private string gameScene;


    private void Update()
    {
        //if (!AudioManager.Instance.isClipPlaying(titleMusic) && titleMusic)
        //{
        //    AudioManager.Instance.PlayClipLooped(titleMusic);
        //}
    }

    public void LoadGame()
    {
        //if (loadLevelSound) AudioManager.Instance.PlayClip(loadLevelSound);

        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
