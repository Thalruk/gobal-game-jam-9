using UnityEngine;

public class HeartPickup : PickUpBase
{
    float x = 0;

    public override void PickUp()
    {
        Player.Instance.ChangeHealth(1);
    }

    void Update()
    {
        x += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, Mathf.Sin(x));
    }
}
