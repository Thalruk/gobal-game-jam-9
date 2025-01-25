using UnityEngine;

public class Heart : MonoBehaviour
{
    public void DestroyHeart()
    {
        Player.Instance.currentHealth--;
        Player.Instance.CheckHealth();
        Destroy(gameObject);
    }
}
