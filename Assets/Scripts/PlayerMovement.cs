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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
