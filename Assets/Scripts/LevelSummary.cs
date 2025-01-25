using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectedScore;
    [SerializeField] TextMeshProUGUI collectedOptionalScore;
    [SerializeField] TextMeshProUGUI allScore;
    [SerializeField] Slider scoreSlider;

    private void Awake()
    {
        collectedScore.text = $"Collected: {LevelManager.Instance.score}/{LevelManager.Instance.scoreMax}";
        collectedOptionalScore.text = $"Collected secrets: {LevelManager.Instance.scoreOptional}/{LevelManager.Instance.scoreMaxOptional}";

        allScore.text = $"Collected: {LevelManager.Instance.score + LevelManager.Instance.scoreOptional}/{LevelManager.Instance.scoreMax + LevelManager.Instance.scoreMaxOptional}";
    }

}
