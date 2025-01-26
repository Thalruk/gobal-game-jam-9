using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] public int score;
    [SerializeField] public int scoreOptional;
    [SerializeField] public int scoreMax;
    [SerializeField] public int scoreMaxOptional;

    [SerializeField] public float timer = 0;
    [SerializeField] public bool timerStopped;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject pauseMenu;
    bool pauseMenuOn = false;

    [SerializeField] public string nextLevel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuOn = !pauseMenuOn;
            if (pauseMenuOn)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1;
            }

        }



        if (!timerStopped)
        {
            timer += Time.deltaTime;
        }
        timerText.text = timer.ToString("0.00");
    }

    public void Resume()
    {
        pauseMenuOn = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
