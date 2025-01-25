using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer vidPlayer;
    [SerializeField] GameObject text;
    [SerializeField] string nextSceneName;
    float x = 0;
    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        x += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            text.SetActive(true);
        }

        if (text.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        print(x);
        print(vidPlayer.length);
        if (x >= vidPlayer.length)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
