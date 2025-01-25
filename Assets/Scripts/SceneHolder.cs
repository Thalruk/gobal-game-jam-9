using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHolder : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI sceneName;
    [SerializeField] public TextMeshProUGUI sceneScore;
    [SerializeField] public TextMeshProUGUI sceneTime;

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
