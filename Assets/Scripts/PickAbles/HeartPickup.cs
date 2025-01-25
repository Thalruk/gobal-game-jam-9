public class HeartPickup : PickUpBase
{

    public override void PickUp()
    {
        Player.Instance.ChangeHealth(1);
    }
}
