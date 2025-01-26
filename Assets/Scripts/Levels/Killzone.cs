using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player.Instance.Die();
        }
    }
}
