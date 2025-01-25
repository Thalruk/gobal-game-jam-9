using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    [SerializeField] GameObject finishStar;

    [SerializeField] TextMeshProUGUI collectedScore;
    [SerializeField] GameObject scoreStar;

    [SerializeField] TextMeshProUGUI collectedOptionalScore;
    [SerializeField] GameObject optionalScoreStar;

    [SerializeField] Slider scoreSlider;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        LevelManager.Instance.timerStopped = true;

        finishStar.SetActive(true);
        collectedScore.text = $"Collected: {LevelManager.Instance.score}/{LevelManager.Instance.scoreMax}";
        if (LevelManager.Instance.score == LevelManager.Instance.scoreMax)
        {
            scoreStar.SetActive(true);
        }

        collectedOptionalScore.text = $"Collected secrets: {LevelManager.Instance.scoreOptional}/{LevelManager.Instance.scoreMaxOptional}";
        if (LevelManager.Instance.scoreOptional == LevelManager.Instance.scoreMaxOptional)
        {
            optionalScoreStar.SetActive(true);
        }

        PlayerPrefs.SetFloat($"{SceneManager.GetActiveScene().name} time", LevelManager.Instance.timer);
        PlayerPrefs.SetFloat($"{SceneManager.GetActiveScene().name} score percent", LevelManager.Instance.score / LevelManager.Instance.scoreMax);
        PlayerPrefs.SetFloat($"{SceneManager.GetActiveScene().name} optional score percent", LevelManager.Instance.scoreOptional / LevelManager.Instance.scoreMaxOptional);
        PlayerPrefs.SetFloat($"{SceneManager.GetActiveScene().name} time", LevelManager.Instance.timer);
        PlayerPrefs.Save();

        //zrobic slider i procenty i gwiazdki w zaleznosci od zebrania
    }

    public void LoadAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadSceneSelectScreen()
    {
        SceneManager.LoadScene("SceneSelection");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelManager.Instance.nextLevel);
    }

}
