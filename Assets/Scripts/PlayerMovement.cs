using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement; 

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; 
    public Animator animator; 
    bool isFacingRight = true; 

    [Header("Movement")]
    public float moveSpeed = 15f; 
    float horizontalMovement; 
    
    [Header("Jumping")]
    public float jumpPower = 10f; 
    public int maxJumps = 2; 
    int jumpsRemaining; 

    [Header("GroundCheck")]
    public Transform groundCheckPos; 
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f); 
    public LayerMask groundLayer; 

    [Header("Gravity")]
    public float baseGravity = 2; 
    public float maxFallSpeed = 18f; 
    public float fallSpeedModifier = 2f; 
    
    [Header("Knockback")]
    public float KBForce; 
    public float KBCounter; 
    public float KBTotalTime; 
    public bool KnockFromRight; 

    void Start()
    {
        
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name; 

        if (KBCounter <= 0) 
        {
            rb.linearVelocity = new Vector2(horizontalMovement *  moveSpeed, rb.linearVelocity.y); 
            if (sceneName == "Lake") 
            {
                GroundCheck(); 
            }
        }
        else
        {
            if (KnockFromRight == true) 
            {
                rb.linearVelocity = new Vector2(-KBForce, KBForce); 
            }
            if (KnockFromRight == false) 
            {
                rb.linearVelocity = new Vector2(KBForce, KBForce); 
            }
            KBCounter -= Time.deltaTime; 
        }
        
        Gravity();
        Flip(); 
        animator.SetFloat("yVelocity",rb.linearVelocity.y); 
        animator.SetFloat("magnitude",rb.linearVelocity.magnitude);
    }

    public void Gravity() 
    {
        if (rb.linearVelocity.y < 0) 
        {
            rb.gravityScale = baseGravity * fallSpeedModifier; 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); 
        }
        else 
        {
            rb.gravityScale = baseGravity; 
        }
    }
    public void Move(InputAction.CallbackContext context) 
    { 
        horizontalMovement = context.ReadValue<Vector2>().x; 
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name; 
        if (sceneName == "Lake")
        {
            if (jumpsRemaining > 0)
            {
                if (context.performed)
                {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                animator.SetTrigger("jump"); 
                jumpsRemaining--; 
                }
                else if (context.canceled) 
                {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); 
                animator.SetTrigger("jump");
                jumpsRemaining--; 
                }
            }
        }

        else if (isGrounded())
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                animator.SetTrigger("jump"); 
            }
            else if (context.canceled) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); 
                animator.SetTrigger("jump");
            }
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true; 
        }
        return false; 
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps; 
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0) 
        { 
            isFacingRight = !isFacingRight; 
            Vector3 ls = transform.localScale; 
            ls.x *= -1f; 
            transform.localScale = ls; 
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white; 
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize); 
    }
}
