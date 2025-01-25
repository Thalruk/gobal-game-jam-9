using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    public abstract void PickUp();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print($"{name} picked up");
            PickUp();
        }
    }
}
