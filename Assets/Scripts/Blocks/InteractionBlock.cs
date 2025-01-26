using UnityEngine;

public class InteractionBlock : Blocks
{
    [SerializeField] bool fly = false;
    [SerializeField] bool up = false;
    [SerializeField] bool down = false;
    [SerializeField] bool destroying = false;
    [SerializeField] bool pushing = false;

    Vector2 startPos;
    Vector2 endPos;

    GameObject bubbleObj;

    float upTime = 0f;
    float flyTime = 0f;
    float destroyingTime = 0f;
    float pushingTime = 0f;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Bubble bubble = collision.gameObject.GetComponent<Bubble>();

            // soap - levitating
            if (bubble.type == 0)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                startPos = transform.position;
                endPos = startPos + new Vector2(0f, 4f);
                bubbleObj = Instantiate(collision.gameObject);
                bubbleObj.tag = "Untagged";
                Destroy(bubbleObj.GetComponent<CircleCollider2D>());
                Destroy(bubbleObj.GetComponent<Rigidbody2D>());
                bubbleObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                bubbleObj.transform.position = transform.position;

                float boxSize = GetComponent<SpriteRenderer>().bounds.size.x;
                boxSize = boxSize * Mathf.Sqrt(2);
                float scaleDiff = boxSize / bubble.GetComponent<SpriteRenderer>().bounds.size.x * 3f;

                bubbleObj.transform.localScale = new Vector2(scaleDiff, scaleDiff);

                bubbleObj.transform.SetParent(transform);

                //fly = true;
                up = true;

            }
            // stone - pushing
            else if (bubble.type == 1)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                print("stone");
                Vector2 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                int direction = vel.x < 0f ? -1 : 1;

                pushing = true;
                pushingTime = 0f;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 300f, 0f));
            }
            // glass - ???
            else if (bubble.type == 2)
            {

            }
            // lava - destroying
            else if (bubble.type == 3)
            {

                Vector2 offset = new Vector2(0f, GetComponent<BoxCollider2D>().size.y / 2f);
                GameObject lava = Instantiate(bubble.GetComponent<LavaBubble>().lavaSpillPrefab);

                float lavaSize = lava.GetComponent<SpriteRenderer>().bounds.size.x;
                float boxSize = GetComponent<SpriteRenderer>().bounds.size.x;
                float sizeDiffrent = boxSize / lavaSize;

                lava.transform.position = (Vector2)transform.position + offset;
                lava.transform.localScale = new Vector2(sizeDiffrent, sizeDiffrent);
                lava.transform.SetParent(transform);

                destroying = true;
            }
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {


        // levitating
        if (up)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            if (upTime > 1f)
            {
                up = false;
                fly = true;
                upTime = 0f;
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, upTime);
                upTime += Time.deltaTime;
            }
        }

        if (fly)
        {
            if (flyTime > 3f)
            {
                fly = false;
                down = true;
                flyTime = 0f;
                Destroy(bubbleObj);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, endPos.y + Mathf.Sin(flyTime) / 5f);
                flyTime += Time.deltaTime;
            }
        }

        if (down)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                down = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            }

        }

        if (destroying)
        {
            if (destroyingTime > 1f)
            {
                destroying = false;
                destroyingTime = 0f;
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = Vector2.Lerp(new Vector2(1f, 1f), Vector2.zero, destroyingTime);
                destroyingTime += Time.deltaTime;
            }
        }

        if (pushing)
        {
            if (pushingTime > 1f)
            {
                pushing = false;
                pushingTime = 0f;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                pushingTime += Time.deltaTime;
            }
        }
    }
}
