using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int dmg;
    public int health;
    public Vector3 attackOffset;
    public float attackRange;
    public LayerMask attackMask;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isWalking;
    public Transform groundCheck;
    public float checkRadious;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        if (moveInput == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }
    }
    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isWalking", isWalking);

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            animator.SetTrigger("Attack");
            Attack();
        }
        if (transform.position.y <= -50)
        {
            GetComponent<Health>().TakeDamage(GetComponent<Health>().numOfHearts * 2);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D collinfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (collinfo != null)
        {
            Debug.Log(collinfo.gameObject);
        }
        if (collinfo != null && collinfo.GetComponent<MushroomDMG>() != null)
        {
            collinfo.GetComponent<MushroomDMG>().TakeDamage(dmg);
        } else if (collinfo != null && collinfo.GetComponent<EnemySlime>() != null)
        {
            collinfo.GetComponent<EnemySlime>().TakeDamage(dmg);
        }
    }
}
