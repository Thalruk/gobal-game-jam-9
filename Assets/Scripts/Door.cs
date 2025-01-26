using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    [SerializeField] Sprite openDoor;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject levelFinishUI;

    AudioSource source;

    bool fade = false;
    float timeFade = 0f;
    bool summarySpawned = false;
    Vector2 startPos;

    private void Awake()
    {
        timeFade = 0f;
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timeFade = 0f;
            startPos = Player.Instance.transform.position;
            GetComponent<SpriteRenderer>().sprite = openDoor;
            fade = true;
            source.Play();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timeFade >= 1f && summarySpawned == false)
        {
            print("UI");
            Instantiate(levelFinishUI, canvas.transform);
            summarySpawned = true;

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
