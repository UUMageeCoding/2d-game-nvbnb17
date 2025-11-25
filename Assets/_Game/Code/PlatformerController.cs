using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    public Rigidbody2D rb;
    private TrailRenderer _trailRenderer;
    private bool isGrounded;
    private float moveInput;

    public float dashDuration = .3f;
    private bool isDashing;
    private bool facingRight;

    public float dashForce = 15f;



     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _trailRenderer = GetComponent<TrailRenderer>();
        
        // Set to Dynamic with gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // dashing mechanic
    isDashing = false; 
    facingRight = true;
        
    }
    
     private void Update()
    {
        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0f)
        {
            facingRight = true;
        }
        else if (moveInput < 0f)
        {
            facingRight = false;
        }
        
    
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Dash") && !isDashing)
        {
            StartCoroutine(Dash());
        }


    }
    
    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
    
    // Visualise ground check in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    IEnumerator Dash()
{
    isDashing = true;

    float dashDirection;

    if (facingRight)
        {
            dashDirection = 1f;
        }
        else
        {
            dashDirection = -1f;
        }
    rb.linearVelocity = new Vector2(dashForce * dashDirection,0f);


     yield return  new WaitForSeconds(dashDuration);

     isDashing = false; 
     rb.linearVelocity = Vector2.zero;
}

}

