using TMPro;
using UnityEngine;

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
        if (!timerStopped)
        {
            timer += Time.deltaTime;
        }
        timerText.text = timer.ToString("0.00");
    }
}
