using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    float x = 0;
    [SerializeField] bool infinite;
    public abstract void PickUp();
    private void Update()
    {
        x += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print($"{name} picked up");
            PickUp();

            if (!infinite)
            {
                Destroy(gameObject);
            }
        }
    }
}
