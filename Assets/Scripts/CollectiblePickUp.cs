public class CollectiblePickUp : PickUpBase
{
    public override void PickUp()
    {
        LevelManager.Instance.score++;
    }
}
