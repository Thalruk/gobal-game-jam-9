using UnityEngine;

public class Spike : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 vel;
    public int damage = 1;
    public int hp = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bubble")
        {
            Bubble bubble = collision.gameObject.GetComponent<Bubble>();
            if (bubble.hp < damage)
            {
                rb.velocity = Vector2.zero;
                Destroy(collision.gameObject);
                rb.velocity = vel;
            }
            else
            {
                bubble.hp -= damage;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(gameObject);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = bubble.vel;
            }
        }

        if (collision.gameObject.tag == "Player")
        {

            Player.Instance.ChangeHealth(-1);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }
}
