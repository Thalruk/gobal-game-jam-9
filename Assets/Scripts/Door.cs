using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite openDoor;

    bool fade = false;
    float timeFade = 0f;
    Vector2 startPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startPos = Player.Instance.transform.position;
            GetComponent<SpriteRenderer>().sprite = openDoor;
            fade = true;
        }
    }

    private void Update()
    {
        if (fade)
        {
            Player.Instance.transform.localScale = Vector2.Lerp(new Vector2(1f, 1f), new Vector2(0.5f, 0.5f), timeFade);
            Player.Instance.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f), timeFade);
            Player.Instance.transform.position = Vector2.Lerp(startPos, transform.position, timeFade);
            timeFade += Time.deltaTime;
        }
    }
}
