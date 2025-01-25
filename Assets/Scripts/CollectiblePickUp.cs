using UnityEngine;

public class CollectiblePickUp : PickUpBase
{
    [SerializeField] public bool optional = false;
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
