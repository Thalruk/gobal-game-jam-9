using System.Collections.Generic;
using UnityEngine;

public class SceneListHolder : MonoBehaviour
{
    [SerializeField] List<string> sceneList;
    [SerializeField] SceneHolder holder;

    private void Awake()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        foreach (string sceneName in sceneList)
        {
            SceneHolder sceneHolder = Instantiate(holder, transform);
            sceneHolder.sceneName.text = sceneName;
            if (PlayerPrefs.GetFloat($"{sceneName} time") != null)
            {
                holder.sceneTime.text = PlayerPrefs.GetFloat($"{sceneName} time").ToString();
                holder.finishStar.SetActive(true);
            }

            if (PlayerPrefs.GetFloat($"{sceneName} score percent") != null)
            {
                if (PlayerPrefs.GetFloat($"{sceneName} score percent") == 1)
                {
                    holder.scoreStar.SetActive(true);
                }
            }

            if (PlayerPrefs.GetFloat($"{sceneName} optional score percent") != null)
            {
                if (PlayerPrefs.GetFloat($"{sceneName} score percent") == 1)
                {
                    holder.scoreOptionalStar.SetActive(true);
                }
            }
        }
    }
}
