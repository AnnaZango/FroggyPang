using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToadMovement : MonoBehaviour
{
    // Controls everything related to movement. Input is taken using the new input system to 
    // allow using two types of controls: mouse+keyboard and gamepad. All input is controlled
    // using Events on Player Input


    [Header("X and Y movement")]
    [SerializeField] float inputX;
    [SerializeField] float inputY;
    [SerializeField] float normalGravity = 7;
    [SerializeField] float speed;
    private bool isFacingRight = true;

    [Header("Dashing variables")]
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] float dashingPower = 25f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;

    [Header("Wall sliding/moving variables")]
    [SerializeField] bool isWallMoving = false;
    [SerializeField] float wallGravity = 0;
    //[SerializeField] float wallSlidingSpeed = 2f; //use if sliding speed instead of gravity
    [SerializeField] float radiusWallCheck = 0.2f;
    [SerializeField] Transform wallCheck;

    [Header("Jumping variables")]
    [SerializeField] float jumpingPower;
    [SerializeField] float jumpingPowerWall;
    [SerializeField] bool isGrounded = true;
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] bool isWallJumping = false;
    [SerializeField] float jumpCounterWall = 0;
    [SerializeField] float jumpCoyoteTimeWall = 0.3f;

    [SerializeField] float jumpCounterGround = 0;
    [SerializeField] float jumpCoyoteTimeGround = 0.2f;


    private Rigidbody2D rb;
    LayerMask layerLimits;

    [SerializeField] Animator animator;

    //sounds:
    [SerializeField] AudioSource jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        layerLimits = (1 << LayerMask.NameToLayer("Limits"));
    }
    

    void Start()
    {
        GameManager.SetGameFinished(false);
    }


    void Update()
    {
        if (GameManager.GetIfGameFinished()) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            return; 
        }

        if (isDashing || isWallJumping) { return; }

        isGrounded = IsGrounded(); //visible in the inspector, for debugging purposes
        
        WallMovement(); //when on a wall, it can also move in the Y direction

        Flip(); //when moving left or right

        if (!IsOnWall())
        {
            if(inputX != 0)
            {
                animator.SetInteger("state", 3);
            }
            else
            {
                animator.SetInteger("state", 0);
            }
        } 

    }

    private void FixedUpdate()
    {
        if (GameManager.GetIfGameFinished()) { return; }
        //if (!GameManager.GetPlayerAlive()) { return; }

        if (isDashing || isWallJumping) { return; }

        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
    }

    

    private void Flip()
    {
        if(isFacingRight && inputX < 0 || !isFacingRight && inputX > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;           
        }
    }

    private bool IsGrounded()
    {
        if (rb.RaycastRayHit(Vector2.down, groundCheckDistance, layerLimits))
        {
            //coyote time to allow jump for a fraction of a second when leaving a platform
            jumpCounterGround = jumpCoyoteTimeGround;
            return true;
        }
        else if (jumpCounterGround < 0)
        {
            return false;
        }
        else
        {
            jumpCounterGround -= Time.deltaTime;
            return true;
        }        
    }

    public void MoveAction(InputAction.CallbackContext context) 
    {
        if(GameManager.GetIfGameFinished()) { return; }

        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }

    public void JumpAction(InputAction.CallbackContext context)
    {
        if (GameManager.GetIfGameFinished()) { return; }

        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetTrigger("jump");
            jumpSound.Play();
        }
        if(isWallMoving && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPowerWall);
            animator.SetTrigger("jump");
            jumpSound.Play();
        }
        if (context.canceled && rb.velocity.y > 0) //jump higher by pressing jump button longer
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void DashAction(InputAction.CallbackContext context)
    {
        //it allows a fast movement in left/right direction
        if (!canDash) { return; }
        StartCoroutine(Dash());
    }

    private bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, radiusWallCheck, layerLimits);
    }

    private void WallMovement()
    {        
        //when on wall, player can move vertically as well, and gravity is less, to simulate sliding
        //down
        if(IsOnWall())
        {
            if(inputY != 0)
            {
                animator.SetInteger("state", 2);
            }
            else
            {
                animator.SetInteger("state", 1);
            }

            isWallMoving = true;
            jumpCounterWall = jumpCoyoteTimeWall;

            //move up and down the wall:
            rb.velocity = new Vector2(rb.velocity.x, inputY * speed);
            rb.gravityScale = wallGravity;
        } 
        else if(jumpCounterWall < 0)
        {
            isWallMoving = false;
        }
        else
        {
            jumpCounterWall -= Time.deltaTime;
            rb.gravityScale = normalGravity; //reset gravity to normal as no longer sticking to wall
        }
    }
        

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "limits")
        {
            //we set velocity to 0 right when we collide to a wall, so player doesn't move up or
            //down, as it sticks to wall.
            rb.velocity = Vector2.zero; 
        }
    }
    

    private IEnumerator Dash()
    {
        //coroutine for dashing, with a cooldown time
        canDash = false;
        isDashing = true;
        float normalGravity = rb.gravityScale;
        rb.gravityScale = wallGravity;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = normalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<ICollectable>() != null)
        {
            other.gameObject.GetComponent<ICollectable>().Collect();
        }
        
    }


}
