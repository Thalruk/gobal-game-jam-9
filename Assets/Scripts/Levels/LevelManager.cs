using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] public int score;
    [SerializeField] int scoreMax;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        scoreMax = FindObjectsOfTypeAll(typeof(CollectiblePickUp)).Length;
    }
}
