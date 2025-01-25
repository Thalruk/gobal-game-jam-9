using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] public int score;
    [SerializeField] public int scoreOptional;
    [SerializeField] public int scoreMax;
    [SerializeField] public int scoreMaxOptional;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Object[] orbs = Resources.FindObjectsOfTypeAll(typeof(CollectiblePickUp));

        foreach (Object item in orbs)
        {
            if (((CollectiblePickUp)item).optional == false)
            {
                scoreMax++;
            }
            else
            {
                scoreMaxOptional++;
            }
        }
    }
}
