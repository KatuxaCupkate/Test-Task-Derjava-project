using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChat : MonoBehaviour
{
    private Rigidbody2D rb;
   
    private Animator animator;
    private Transform currentPlatform;
    private CircleCollider2D characterCollider;
    private PlayerLife playerLife;
    private PlayerCombat playerCombat;



    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Transform m_GroundCheck;

    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask  groundLayer;

    [SerializeField] private float climbForce = 5.0f;
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float jumpForce = 5.0f;

    [SerializeField] private Collider2D m_CrouchDisableCollider;


    const float k_CeilingRadius = .7f;
    private float horizontalInput;
    private Vector2 move;
    private bool isNearPlatform;
    
    private bool isGround;
    private bool isDead;
    private bool freezed;
    private bool isCrouching;
    private bool isDownWalk;
    private bool isPullingUp;
    private bool jumpHeld;
    public bool m_FacingRight = true;



    private enum MovementState { Idle, Run, Jump, Crouching, PullUp, IsDownWalk }
    private MovementState state;

    [SerializeField] private AudioSource jumpSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        animator = GetComponent<Animator>();
        characterCollider = GetComponent<CircleCollider2D>();
        playerLife = GetComponent<PlayerLife>();
        playerCombat = GetComponent<PlayerCombat>();
       

    }
    private void FixedUpdate()
    {
        // Check if the character is grounded
        isGround = Physics2D.IsTouchingLayers(characterCollider, groundLayer);

        isDead = playerLife.isDead;
        
        
        // Check if the character is near a platform
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_CeilingRadius, platformLayer);
        isNearPlatform = colliders.Length > 0;
        if (isNearPlatform)
        {
            currentPlatform = colliders[0].transform;

        }

    }

    void Update()
    {
        if (!isDead && !freezed)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            PlayerMove();
            UpdateAnimationState();

        }
    }


        
        
    private void UpdateAnimationState()
    {
        MovementState state;
        if (horizontalInput > .1f)
        {
            state = MovementState.Run;
        }
        else if (horizontalInput < -.1f)
        {
            state = MovementState.Run;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.Jump;
        }

        if (isCrouching && !isDownWalk)
            state = MovementState.Crouching;
        if (isCrouching && isDownWalk)
            state = MovementState.IsDownWalk;

        if (isPullingUp)
        {
            state = MovementState.PullUp;
            isPullingUp = false;
        }
        animator.SetInteger("State", (int)state);
    }
    public void PlayerMove()
    {
        if (!isDead && !isPullingUp)
        {
            move = transform.position;
            move.x += speed * horizontalInput * Time.deltaTime;
            transform.position = move;
        }
        if (horizontalInput > .1f && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        else if (horizontalInput < -.1f && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            isGround = false;
            isPullingUp = false;


        }
        jumpHeld = (!isGround && Input.GetButton("Jump")) ? true : false;

        //PullUp

        if (isNearPlatform && jumpHeld)
        {
            if (currentPlatform != null)
            {
                isPullingUp = true;
                rb.velocity = new Vector2(rb.velocity.x, climbForce);
                isGround = true;
                isNearPlatform = false;
                currentPlatform = null;

            }
        }

        //Crouch
        if (Input.GetButtonDown("Down") && isGround)
        {
            isCrouching = true;

        }
        else if (Input.GetButtonUp("Down"))
        {
            isCrouching = false;
            isDownWalk = false;
            m_CrouchDisableCollider.enabled = true;
        }
        if (isCrouching && (horizontalInput > .1f || horizontalInput < -.1f))
        {
            isDownWalk = true;
            m_CrouchDisableCollider.enabled = false;

        }
        else
        { isDownWalk = false; }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
   public void Freeze(bool freez)
    {

        animator.SetInteger("State",(int)MovementState.Idle);
        freezed = freez;
        rb.velocity = Vector2.zero;
        // TODO add more mecanic maybe
    }
}


