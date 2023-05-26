using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isJumping = false;
    private Rigidbody2D charBody;
    private Animator anim;
    private bool facingRight = true;

    void Start()
    {
        charBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        charBody.velocity = new Vector2(movement.x * speed, charBody.velocity.y);

        // Flip character direction
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        // Here we set the speed parameter in the animator
        anim.SetFloat("Speed", Mathf.Abs(charBody.velocity.x));

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            charBody.velocity = new Vector2(charBody.velocity.x, jumpForce);
            isJumping = true;
            anim.SetBool("IsJumping", true);
        }
        else if (charBody.velocity.y < 0)
        {
            // This makes the character fall faster than they rise
            charBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (charBody.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            // This makes the character start falling if the jump button is not held
            charBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            anim.SetBool("IsJumping", false);

            // Calculate and check surface angle
            CalculateSurfaceAngle(collision);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CalculateSurfaceAngle(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 surfaceNormal = contact.normal;
        float surfaceAngle = Vector2.Angle(Vector2.up, surfaceNormal);
        Debug.Log("Surface Angle: " + surfaceAngle);
    }
}
