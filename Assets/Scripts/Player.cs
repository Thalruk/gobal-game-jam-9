using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] public int ammo = 0;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] Slider ammoSlider;
    bool canShoot = true;
    [SerializeField] float chargedAmount = 0f;
    [SerializeField] Slider chargeSlider;
    [SerializeField] GameObject[] bubbles;
    [Space]
    [Header("Health")]
    [SerializeField] GameObject heartHolder;
    [SerializeField] GameObject heart;
    [SerializeField] int startingHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] bool invincible = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        ChangeHealth(startingHealth);
        ChangeAmmo(maxAmmo);
        ammoSlider.maxValue = maxAmmo;

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

        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            canShoot = true;
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            chargedAmount = Mathf.Clamp(chargedAmount + Time.deltaTime, 0, 1);
            chargeSlider.value = chargedAmount;
        }
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            GameObject bubbleObject = Instantiate(bubbles[activeBubble], transform.position, Quaternion.identity);
            Bubble bubble = bubbleObject.GetComponent<Bubble>();
            if (ammo >= bubble.ammoCost)
            {
                Rigidbody2D bubbleRigidbody2D = bubbleObject.GetComponent<Rigidbody2D>();

                bubble.charged = (chargedAmount >= 1f);
                chargedAmount = 0;
                chargeSlider.value = chargedAmount;
                bubbleRigidbody2D.velocity = (direction.x < 0f) ? Vector2.left : Vector2.right;
                bubbleRigidbody2D.velocity += (Vector2.right * horizontal);

                bubble.Init();
                ChangeAmmo(-bubble.ammoCost);
                if (ammo <= 0)
                {
                    canShoot = false;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("trap"))
        {
            ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int value)
    {

        if (value < 0 && invincible == false)
        {
            heartHolder.transform.GetChild(heartHolder.transform.childCount - 1).GetComponent<Animator>().Play("HeartBoom");
            invincible = true;
            Invoke(nameof(TurnOffInvicibility), 0.5f);
        }

        if (value > 0)
        {
            currentHealth += value;
            for (int i = 0; i < value; i++)
            {
                Instantiate(heart, heartHolder.transform);
            }
        }
    }


    public void ChangeAmmo(int value)
    {
        ammo = Mathf.Clamp(ammo + value, 0, maxAmmo);
        ammoSlider.value = ammo;
    }

    public void CheckHealth()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void TurnOffInvicibility()
    {
        invincible = false;
    }
}
