using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    [SerializeField] bool infinite;
    [SerializeField] protected AudioClip collectSound;

    private void Awake()
    {
    }

    public abstract void PickUp();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player.Instance.source.clip = collectSound;
            print($"{name} picked up");
            PickUp();
            Player.Instance.source.Play();

            if (!infinite)
            {
                Destroy(gameObject);
            }
        }
    }
}
