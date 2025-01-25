using UnityEngine;

public class AmmoPickUp : PickUpBase
{
    [SerializeField] int ammoAmount;
    [SerializeField] int ammoType;
    public override void PickUp()
    {
        Player.Instance.ChangeAmmo(ammoType, ammoAmount);
    }
}
