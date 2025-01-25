using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject spikePrefab;
    bool fly = false, pushBack = false;
    public int damage = 1;
    public int hp = 10;
    bool canShoot = true;
    float shootDelay = 3f;
    float timeFly = 0f, timePush = 0f;
    Vector2 startPosition, endPosition, pushVector;
    Rigidbody2D rb;
    [SerializeField] float walkTime = 0f;
    float lavaCooldown = 0f;
    bool hitted = false;
    float timeHitted = 0f;
    float rotationTime = 0f;
    [SerializeField] Sprite[] enemySprites;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pushVector = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            rb.gravityScale = 0f;
            //transform.position = new Vector2(transform.position.x + pushVector.x, transform.position.y + pushVector.y);
            //pushVector.y = Mathf.Lerp(0.01f, 0f, timeFly);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(10f, 0f, timeFly));
            timeFly += Time.deltaTime;
            if (rb.velocity.y == 0f)
            {
                if (gameObject.transform.childCount > 0)
                {
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                }
                GetComponent<Rigidbody2D>().gravityScale = 50f;
                fly = false;
                pushVector.y = 0f;
            }
        }
        if (pushBack)
        {
            //transform.position = new Vector2 (transform.position.x + pushVector.x, transform.position.y + pushVector.y);
            //pushVector.x = Mathf.Lerp(0.5f, 0f, timePush);
            timePush += Time.deltaTime;
            if (rb.velocity.x == 0f)
            {
                pushBack = false;
                pushVector.x = 0f;
            }
        }

        if(Vector2.Distance(Player.Instance.transform.position, transform.position) < 5f)
        {
            if (!hitted)
            {
                Shoot();
            }
            else
            {
            timeHitted -= Time.deltaTime;
            if (timeHitted < 0f)
                hitted = false;
            }
        }
        else
        {
            if (!hitted)
            {
                Patrol();
            }
            else
            {
                timeHitted -= Time.deltaTime;
                if (timeHitted < 0f)
                    hitted = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bubble")
        {
            hp -= collision.gameObject.GetComponent<Bubble>().damage;
            if(hp <= 0f)
            {
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            float hitDirection = (collision.gameObject.transform.position - transform.position).x;
            hitDirection = hitDirection < 0 ? 1 : -1;
            hitted = true;
            timeHitted = 1f;
            rb.velocity = new Vector2(1f * hitDirection, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            lavaCooldown = 1f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            lavaCooldown -= Time.deltaTime;
            if (lavaCooldown < 0f)
            {
                lavaCooldown = 1f;
                hp -= (collision.gameObject.transform.parent.GetComponent<LavaSpill>().damage);
                if (hp <= 0f)
                    Destroy(gameObject);
            }
        }
    }

    public void GetDamage(int damage) { }
    public void FlyAway()
    {
        timeFly = 0f;
        pushVector.y += 1f;

        fly = true;
        hitted = true;
        timeHitted = 1f;
        rb.velocity = (new Vector2(0f, pushVector.y));

    }

    public void PushBack(Vector2 _pushVector)
    {
        timePush = 0f;
        pushVector.x += _pushVector.x * 200f;
        startPosition = transform.position;
        endPosition = startPosition + pushVector;

        rb.AddForce(new Vector2(pushVector.x, 0f));

        pushBack = true;
        //print(pushVector);
    }

    private void Patrol()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprites[0];
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        rb.velocity = new Vector2(Mathf.Sin(walkTime) * 3f, 0f);
        walkTime = (walkTime + Time.deltaTime) % (Mathf.PI * 2f);
        if(walkTime < Mathf.PI)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rotationTime -= Time.deltaTime * 360f;

        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            rotationTime += Time.deltaTime * 360f;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotationTime);
    }

    private void Shoot()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprites[1];
        transform.rotation = Quaternion.Euler(Vector2.zero);
        float direction = Player.Instance.transform.position.x - transform.position.x;
        direction = direction < 0f ? -1f : 1f;
        GetComponent<SpriteRenderer>().flipX = direction < 0f ? false : true;
        rb.velocity = Vector2.zero;
        if (canShoot)
        {
            canShoot = false;
            shootDelay = 3f;
            
            GameObject spikeObject = Instantiate(spikePrefab, transform.position, Quaternion.identity);
            spikeObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f * direction);
            spikeObject.GetComponent<Spike>().vel = Vector2.right * direction * 5;
            //spikeObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * 5;
        }
        else if(shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
        else
        {
            shootDelay = 0f;
            canShoot = true;
        }
    }
}
