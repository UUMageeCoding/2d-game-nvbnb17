using System.Collections;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
     public Rigidbody2D rb;
     private float moveInput;
    public float dashDuration = .3f;
    private bool isDashing;
    private bool facingRight;

    public float dashForce = 15f;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();

        isDashing = false; 
         facingRight = true;

    }

    private void Update()
    {
         moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0f)
        {
            facingRight = true;
        }
        else if (moveInput < 0f)
        {
            facingRight = false;
        }

        if (Input.GetMouseButtonDown(0) && !isDashing)
        {
            StartCoroutine(Dash());
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
