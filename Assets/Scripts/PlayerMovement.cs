using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;

    Rigidbody2D rb;

    [SerializeField] int speed;
    [SerializeField] int jumpStrength;
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] GameObject graphics;

    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] bool isGrounded = false;

    int activeBubble = 0;
    int ammo = 10;
    bool canShoot = true;
    float chargedAmmount = 0f;
    [SerializeField] GameObject[] bubbles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(10, 6);
        Physics2D.IgnoreLayerCollision(10, 9);
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

        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.transform.position, groundCheckRadius);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength * 0.5f);
        }
        if (Input.GetMouseButton(0) && canShoot)
        {
            chargedAmmount += Time.deltaTime;
            print(chargedAmmount);
        }
        if(Input.GetMouseButtonUp(0) && canShoot)
        {
            GameObject bubble = Instantiate(bubbles[activeBubble], transform.position, Quaternion.identity);
            bubble.GetComponent<Bubble>().charged = (chargedAmmount > 1f);
            chargedAmmount = 0;
            bubble.GetComponent<Rigidbody2D>().velocity = (direction.x < 0f) ? Vector2.left : Vector2.right;
            
            bubble.GetComponent<Rigidbody2D>().velocity += (Vector2.right * horizontal);
            bubble.GetComponent<Bubble>().Init();
            ammo -= bubble.GetComponent<Bubble>().ammoCost;
            print(bubble.GetComponent<Bubble>().ammoCost);
            if(ammo <= 0)
            {
                canShoot = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckTransform.transform.position, groundCheckRadius);
    }
}
