using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer vidPlayer;
    [SerializeField] GameObject text;
    [SerializeField] string nextSceneName;

    private void Awake()
    {
        vidPlayer.Prepare();
        Cursor.visible = false;
    }

    private void Update()
    {
        if (vidPlayer.isPrepared)
        {
            vidPlayer.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            text.SetActive(true);
        }

        if (text.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
