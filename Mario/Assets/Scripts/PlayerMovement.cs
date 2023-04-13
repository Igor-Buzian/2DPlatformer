using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;
    public Transform fontCheck;

    private float dirX = 0f;
    private bool isWallFront = false;
    private enum MovementState {idle, running, jumping, falling  }

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private float jumpForce = 12f;

    
    private bool isWallSliding = false; // Переменная, показывающая, скользит ли игрок по стене
    private bool isWallJumping = false;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    //private Vector2 wallNormal;
    public float wallSlideSpeed = .5f; // Скорость скольжения по стене
    public float wallDistance = .7f; // Расстояние, на котором игрок может прыгнуть от стены
    public LayerMask wallLayerMask; // Слой, на котором находятся стены

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        box = GetComponent<BoxCollider2D>();
        jumpSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter");
    }

    // Update is called once per frame
    private void Update()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            Run();            
            Jump();
            WallJump();
            if (!isWallJumping)
            {
                Flip();
            }
            WalledSlide();
            UpdateAnimation();
        }
        else
        {
            animator.SetInteger("state", (int)MovementState.idle);
        }
    }
    private void UpdateAnimation()
    {
        MovementState state;
        
            if (dirX > 0f)
            {
                state = MovementState.running;  
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;                
            }
            else
            {
                state = MovementState.idle;
            }
            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = MovementState.falling;
            }            
        animator.SetInteger("state", (int)state);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Run()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            //spriteRenderer.flipX = false; 
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(fontCheck.position, .2f, wallLayerMask);
    }
    private void WalledSlide()
    {
        if(IsWalled() && !IsGrounded() && dirX != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else { isWallSliding = false; }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            //wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            UnityEngine.Debug.Log($"isWallSliding: {isWallSliding}");
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            UnityEngine.Debug.Log($"isWallSliding: {isWallSliding}");
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
           /* if (transform.localScale.x != wallJumpingDirection)
            {
                //isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }*/

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    /*    private void FixedUpdate()
        {
            // Проверяем, касается ли игрок стены
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, -transform.right, wallDistance, wallLayerMask);
            if (wallHit.collider != null)
            {
                // Определяем нормаль стены
                wallNormal = wallHit.normal;
                // Если игрок движется в сторону стены, то он начинает скользить по ней
                if (Mathf.Sign(rb.velocity.x) == Mathf.Sign(wallNormal.x))
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                    isWallSliding = true;
                }
                else
                {
                    isWallSliding = false;
                }
            }
            else
            {
                isWallSliding = false;
            }
        }*/
}