using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    float horizontal;

    Rigidbody2D rb;

    [SerializeField] int speed;
    [SerializeField] int jumpStrength;
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] GameObject graphics;
    [Space]
    [Header("GroundCheck")]
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask mask;
    [Space]
    [Header("Ammo")]
    [SerializeField] int activeBubble = 0;
    [SerializeField] int ammo = 10;
    bool canShoot = true;
    float chargedAmmount = 0f;
    [SerializeField] GameObject[] bubbles;
    [Space]
    [Header("Health")]
    [SerializeField] GameObject heartHolder;
    [SerializeField] GameObject heart;
    [SerializeField] int startingHealth;
    [SerializeField] public int currentHealth;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        ChangeHealth(startingHealth);

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heart, heartHolder.transform);
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 1)
        {
            direction = Vector2.right;
            graphics.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontal == -1)
        {
            direction = Vector2.left;
            graphics.GetComponent<SpriteRenderer>().flipX = true;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.transform.position, groundCheckRadius, mask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }


        if (Input.GetMouseButton(0) && canShoot)
        {
            chargedAmmount = Mathf.Clamp(chargedAmmount + Time.deltaTime, 0, 1);
        }
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            GameObject bubbleObject = Instantiate(bubbles[activeBubble], transform.position, Quaternion.identity);
            Bubble bubble = bubbleObject.GetComponent<Bubble>();
            if (ammo >= bubble.ammoCost)
            {
                Rigidbody2D bubbleRigidbody2D = bubbleObject.GetComponent<Rigidbody2D>();

                bubble.charged = (chargedAmmount >= 1f);
                chargedAmmount = 0;
                bubbleRigidbody2D.velocity = (direction.x < 0f) ? Vector2.left : Vector2.right;
                bubbleRigidbody2D.velocity += (Vector2.right * horizontal);

                bubble.Init();
                ammo -= bubble.ammoCost;
                print(bubble.ammoCost);
                if (ammo <= 0)
                {
                    canShoot = false;
                }
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            activeBubble = (activeBubble + 1) % 4;
            print("Active bubble: " + activeBubble);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            activeBubble = activeBubble - 1 < 0 ? 3 : activeBubble - 1;
            print("Active bubble: " + activeBubble);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
