using System.Collections;
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
    
    private Rigidbody2D rb;

    private TrailRenderer _trailRenderer;
    private bool isGrounded;
    private float moveInput;
    
    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 15.5f;

    [SerializeField] private float _dashingTime = 0.1f;

    private Vector2 _dashingDir;

    private bool _isDashing;

    private bool _canDash = true;


     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _trailRenderer = GetComponent<TrailRenderer>();
        
        // Set to Dynamic with gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
     private void Update()
    {
        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");
        
        Debug.Log("_isDashing  in update " + _isDashing);
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //dash input

        var dashInput = Input.GetButtonDown("Dash");
        Debug.Log("i Dashed" + dashInput);

        if (dashInput && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x, y:0);
            }
        }

        if (_isDashing)
        {
            rb.linearVelocity = _dashingDir.normalized * _dashingVelocity;
            return;
        }

        if (isGrounded)
        {
            _canDash = true;
        }

        StartCoroutine(StopDashing());
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

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        Debug.Log("in coroutine isdashing" + _isDashing);
    }
}