using System.Collections.Generic;
using UnityEngine;

public class SceneListHolder : MonoBehaviour
{
    [SerializeField] List<string> sceneList;
    [SerializeField] SceneHolder holder;


    private void Awake()
    {
        foreach (string sceneName in sceneList)
        {
            SceneHolder sceneHolder = Instantiate(holder, transform);
            sceneHolder.sceneName.text = sceneName;
            sceneHolder.sceneScore.text = $"{2}/{30}";
            sceneHolder.sceneTime.text = 12.34f.ToString();
        }
    }
}
