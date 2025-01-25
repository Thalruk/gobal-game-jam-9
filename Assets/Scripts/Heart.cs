using UnityEngine;

public class Heart : MonoBehaviour
{
    public void DestroyHeart()
    {
        Player.Instance.currentHealth--;
        Destroy(gameObject);
    }
}
