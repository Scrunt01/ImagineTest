using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    [SerializeField]
    private GameObject pauseMenuGO;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {
        DevExit();
        if (pauseMenuGO) HandlePauseMenu();
        
    }

    private void HandlePauseMenu()
    {
        PauseMenu pauseMenu = pauseMenuGO.GetComponent<PauseMenu>();

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuGO.activeSelf)
        {
            pauseMenu.OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuGO.activeSelf)
        {
            pauseMenu.ClosePauseMenu();
        }
    }

    
    public void DevExit()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            GoToNextScene();
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
