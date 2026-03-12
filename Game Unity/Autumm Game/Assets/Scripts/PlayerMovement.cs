using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

  private void Update()
{
    horizontalInput = Input.GetAxis("Horizontal");

    // Flip
    if (horizontalInput > 0.01f) transform.localScale = Vector3.one;
    else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

    // Animator
    bool grounded = IsGrounded();
    anim.SetBool("Run", horizontalInput != 0);
    anim.SetBool("grounded", grounded);

    // 🔊 WALK SOUND (Loop): sofort an/aus
    bool walking = grounded && Mathf.Abs(horizontalInput) > 0.01f;
    if (AudioManager.Instance != null)
    {
        if (walking) AudioManager.Instance.StartWalk();
        else AudioManager.Instance.StopWalk();
    }

    // Movement / Wall jump logic
    if (wallJumpCooldown > 0.2f)
    {
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (OnWall() && !grounded)
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 4;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Jump(grounded);
    }
    else
    {
        wallJumpCooldown += Time.deltaTime;
    }
}

    private void Jump(bool grounded)
    {
        if (grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();
        }
        else if (OnWall())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();

            wallJumpCooldown = 0;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return hit.collider != null;
    }
}