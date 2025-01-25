using UnityEngine;

public class AmmoPickUp : PickUpBase
{
    [SerializeField] int ammoAmount;
    float x = 0;
    public override void PickUp()
    {
        Player.Instance.ChangeAmmo(ammoAmount);
    }


    void Update()
    {
        x += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, Mathf.Sin(x));
    }
}
