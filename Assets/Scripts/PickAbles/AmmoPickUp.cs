using UnityEngine;

public class AmmoPickUp : PickUpBase
{
    [SerializeField] int ammoAmount;
    public override void PickUp()
    {
        Player.Instance.ChangeAmmo(ammoAmount);
    }
}
