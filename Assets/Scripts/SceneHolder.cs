using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHolder : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI sceneName;
    [SerializeField] public TextMeshProUGUI sceneTime;
    [SerializeField] public GameObject finishStar;
    [SerializeField] public GameObject scoreStar;
    [SerializeField] public GameObject scoreOptionalStar;

    private void Start()
    {
        if (sceneName != null)
        {
            GetComponent<Button>().onClick.AddListener(LoadLevel);
        }
        else
        {
            Debug.LogError($"Missing scene in {name}");
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(sceneName.text);
    }

}
