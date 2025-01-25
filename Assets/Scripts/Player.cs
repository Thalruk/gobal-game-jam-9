using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public static Player Instance;
    float horizontal;

    Rigidbody2D rb;

    public AudioSource source;

    [SerializeField] int speedDefault;
    [SerializeField] int speed;
    [SerializeField] int jumpStrength;
    [SerializeField] public Vector2 direction = Vector2.right;
    [SerializeField] GameObject graphics;
    bool canMove = true;
    [Space]
    [Header("GroundCheck")]
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask mask;
    [Space]
    [Header("Ammo")]
    [SerializeField] public int activeBubble = 0;
    [SerializeField] public int[] ammo;
    [SerializeField] public int maxAmmo = 32;
    [SerializeField] public Slider ammoSlider;
    bool canShoot = true;
    [SerializeField] GameObject[] bubbles;
    [SerializeField] Image ammoFillImage;
    [SerializeField] Sprite[] ammoFillImages;
    [SerializeField] float refillAmmoTime = 0f;
    [SerializeField] float restDelay = 1f;

    Bubble bubbleShield;
    GameObject bubbleShieldObj;
    bool isShield;
    float shieldCooldown = 0f;
    bool hitted = false;
    float timeHitted = 0f;

    [Space]
    [Header("Health")]
    [SerializeField] GameObject heartHolder;
    [SerializeField] GameObject heart;
    [SerializeField] int startingHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] bool invincible = false;
    private void Awake()
    {
        ammo = new int[4] { 0, 0, 0, 0 };
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        speed = speedDefault;

        rb = GetComponent<Rigidbody2D>();
        ChangeHealth(startingHealth);
        ChangeAmmo(0, maxAmmo);
        ammoSlider.maxValue = maxAmmo;
        ammoFillImage.sprite = ammoFillImages[activeBubble];


    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 1 && canMove)
        {
            direction = Vector2.right;
            graphics.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontal == -1 && canMove)
        {
            direction = Vector2.left;
            graphics.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontal == 0 && restDelay <= 0f)
        {
            refillAmmoTime += Time.deltaTime;
            if (refillAmmoTime > 1f)
            {
                ammo[0] = Mathf.Clamp(ammo[0] + 1, 0, maxAmmo);
                refillAmmoTime = 0f;
                if (activeBubble == 0)
                {
                    ammoSlider.value = ammo[0];
                }
            }
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
            ammoFillImage.sprite = ammoFillImages[activeBubble];
            ChangeAmmo(activeBubble, 0);

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            activeBubble = activeBubble - 1 < 0 ? 3 : activeBubble - 1;
            print("Active bubble: " + activeBubble);
            ammoFillImage.sprite = ammoFillImages[activeBubble];
            ChangeAmmo(activeBubble, 0);
        }

        if (Input.GetMouseButtonDown(0) && ammo[activeBubble] > 0 && !isShield)
        {
            canShoot = true;
        }

        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            restDelay = 1f;
            Vector2 pos = (Vector2)transform.position;
            GameObject bubbleObject = Instantiate(bubbles[activeBubble], pos, Quaternion.identity);
            Bubble bubble = bubbleObject.GetComponent<Bubble>();
            if (ammo[activeBubble] >= bubble.ammoCost)
            {
                Rigidbody2D bubbleRigidbody2D = bubbleObject.GetComponent<Rigidbody2D>();

                bubbleRigidbody2D.velocity = (direction.x < 0f) ? Vector2.left : Vector2.right;
                bubbleRigidbody2D.velocity += (Vector2.right * horizontal);
                bubble.vel = bubbleRigidbody2D.velocity;
                bubble.Init();
                ChangeAmmo(activeBubble, -bubble.ammoCost);
                if (ammo[activeBubble] <= 0)
                {
                    canShoot = false;
                }
            }
        }
        if (Input.GetKey(KeyCode.Q) && !isShield && shieldCooldown == 0f)
        {
            bubbleShieldObj = Instantiate(bubbles[activeBubble], transform.position, Quaternion.identity);
            Destroy(bubbleShieldObj.GetComponent<Rigidbody2D>());
            bubbleShield = bubbleShieldObj.GetComponent<Bubble>();
            bubbleShield.Init();
            bubbleShield.Shield();
            isShield = true;
            canShoot = false;
            canMove = false;
            speed = 0;
        }
        if (Input.GetKeyUp(KeyCode.Q) && isShield)
        {
            RemoveShield();
        }
        shieldCooldown = Mathf.Clamp(shieldCooldown - Time.deltaTime, 0f, 3f);
        restDelay = Mathf.Clamp(restDelay - Time.deltaTime, 0f, 1f);
    }

    private void FixedUpdate()
    {
        if (!hitted)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            timeHitted -= Time.deltaTime;
            if (timeHitted < 0f)
                hitted = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("trap"))
        {
            ChangeHealth(-1);
        }
        if (collision.gameObject.tag == "enemy")
        {
            float hitDirection = (collision.gameObject.transform.position - transform.position).x;
            hitDirection = hitDirection < 0 ? 1 : -1;

            //canMove = false;
            //speed = 0;
            //rb.AddForce(new Vector2(1000f * hitDirection, 100f));

            hitted = true;
            timeHitted = 1f;
            rb.velocity = new Vector2(3f * hitDirection, 3f);

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


    public void ChangeAmmo(int type, int value)
    {
        ammo[type] = Mathf.Clamp(ammo[type] + value, 0, maxAmmo);
        ammoSlider.value = ammo[type];
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

    public void RemoveShield()
    {
        bubbleShield.isShield = false;
        Destroy(bubbleShieldObj);
        isShield = false;
        canShoot = true;
        canMove = true;
        speed = speedDefault;
        shieldCooldown = 3f;
    }
}
