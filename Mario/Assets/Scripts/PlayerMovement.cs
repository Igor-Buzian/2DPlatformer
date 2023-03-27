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
/*
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
        WallCheckRadiusDown = WallCheckDown.GetComponent<CircleCollider2D>().radius;
        gravityDef = rb.gravityScale;
    }
    void Update()
    {
        Reflect();
        Jump();
        Walk();
        MoveOnWall();
        WallJump();
        LedgeGo();
    }
    private void FixedUpdate()
    {
        CheckingGround();
        CheckingWall();
        CheckingLedge();
    }
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        if (blockMoveXforJump || blockMoveXYforLedge)
        {
            moveVector.x = 0;
        }
        else
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public int jumpForce = 10;
    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            anim.StopPlayback();
            anim.Play("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    public bool onWall;
    public bool onWallUp;
    public bool onWallDown;
    public LayerMask Wall;
    public Transform WallCheckUp;
    public Transform WallCheckDown;
    public float WallCheckRayDistance = 1f;
    private float WallCheckRadiusDown;
    public bool onLedge;
    public float ledgeRayCorrectY = 0.5f;
    void CheckingWall()
    {
        onWallUp = Physics2D.Raycast
        (
        WallCheckUp.position,
        new Vector2(transform.localScale.x, 0),
        WallCheckRayDistance,
        Wall
        );
        onWallDown = Physics2D.OverlapCircle(WallCheckDown.position, WallCheckRadiusDown, Wall);
        onWall = (onWallUp && onWallDown);
        anim.SetBool("onWall", onWall);
        if (onWallUp && !onWallDown) { anim.SetBool("wallCheckUp", true); }
        else { anim.SetBool("wallCheckUp", false); }
    }
    void CheckingLedge()
    {
        if (onWallUp)
        {
            onLedge = !Physics2D.Raycast
            (
            new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY),
            new Vector2(transform.localScale.x, 0),
            WallCheckRayDistance,
            Wall
            );
        }
        else { onLedge = false; }
        anim.SetBool("onLedge", onLedge);

        if ((onLedge && Input.GetAxisRaw("Vertical") != -1) || blockMoveXYforLedge)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            offsetCalculateAndCorrect();
        }
    }
    public float minCorrectDistance = 0.01f;
    public float offsetY;
    void offsetCalculateAndCorrect()
    {
        offsetY = Physics2D.Raycast
        (
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY),
        Vector2.down,
        ledgeRayCorrectY,
        Ground
        ).distance;
        if (offsetY > minCorrectDistance * 1.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - offsetY +
            minCorrectDistance, transform.position.z);
        }
    }
    private bool blockMoveXYforLedge;
    void LedgeGo()
    {
        if (onLedge && Input.GetKeyDown(KeyCode.UpArrow))
        {
            blockMoveXYforLedge = true;
            if (onWallUp && !onWallDown) { anim.Play("platformLedgeClimb"); }
            else { anim.Play("wallLedgeClimb"); }
        }
    }
    public Transform finishLedgePosition;
    void FinishLedge()
    {
        transform.position = new Vector3(finishLedgePosition.position.x, finishLedgePosition.position.y,
        finishLedgePosition.position.z);
        anim.Play("idle");
        blockMoveXYforLedge = false;
    }
    public float upDownSpeed = 4f;
    public float slideSpeed = 0;
    private float gravityDef;
    void MoveOnWall()
    {
        if (onWall && !onGround)
        {
            moveVector.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("UpDown", moveVector.y);
            if (!blockMoveXforJump && moveVector.y == 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, slideSpeed);
            }

            if (!blockMoveXYforLedge)
            {
                if (moveVector.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed / 2);
                }
                else if (moveVector.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed);
                }
            }
        }
        else if (!onGround && !onWall) { rb.gravityScale = gravityDef; }
    }
    private bool blockMoveXforJump;
    public float jumpWallTime = 0.5f;
    private float timerJumpWall;
    public Vector2 jumpAngle = new Vector2(3.5f, 10);
    void WallJump()
    {
        if (onWall && !onGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveXforJump = true;
            moveVector.x = 0;
            anim.StopPlayback();
            anim.Play("wallJump");
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
            rb.gravityScale = gravityDef;
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(transform.localScale.x * jumpAngle.x, jumpAngle.y);
        }
        if (blockMoveXforJump && (timerJumpWall += Time.deltaTime) >= jumpWallTime)
        {
            if (onWall || onGround || Input.GetAxisRaw("Horizontal") != 0)
            {
                blockMoveXforJump = false;
                timerJumpWall = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine
        (
        WallCheckUp.position,
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y)
        );
        Gizmos.color = Color.red;
        Gizmos.DrawLine
        (
        new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY),

        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY)
        );
        Gizmos.color = Color.green;
        Gizmos.DrawLine
        (
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY),
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y)
        );
    }
}*/