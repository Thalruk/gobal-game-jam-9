using UnityEngine;

public class CollectiblePickUp : PickUpBase
{
    [SerializeField] public bool optional = false;
    public override void PickUp()
    {
        LevelManager.Instance.score++;
    }
}
