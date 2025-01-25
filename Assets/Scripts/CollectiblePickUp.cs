using UnityEngine;

public class CollectiblePickUp : PickUpBase
{
    [SerializeField] public bool optional = false;

    private void Start()
    {
        if (optional)
        {
            LevelManager.Instance.scoreMaxOptional++;
        }
        else
        {
            LevelManager.Instance.scoreMax++;
        }
    }
    public override void PickUp()
    {
        if (optional)
        {
            LevelManager.Instance.scoreOptional++;
        }
        else
        {
            LevelManager.Instance.score++;
        }
    }
}
