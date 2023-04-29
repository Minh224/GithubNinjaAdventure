using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Playercontroller : MonoBehaviour
{

    public static Playercontroller instance;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private int amountOfjumpLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;
    

    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;



    private float movemenInputDirection;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;


    public float speed = 10.0f;
    public float jump = 16.0f;
    public float GroundCheckRadius;
    public float wallCheckDistance;


    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float turnTimerSet = 0.1f;

    public float dashTime;

    public float distanceBetweenImages;
    public float dashCoolDown;

    public int amountOfJump = 1;

    private bool isFacingRight = true;
    private bool IsRunning;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isDashing;
    private bool knockback;
    private float dashSpeed = 24f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float wallJumpTimerSet = 0.1f;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private bool canNormalJump;
    private bool canWallJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    private Animator anim;


    public Transform GroundCheck;
    public Transform wallCheck;

    public LayerMask WhatisGround;

    private Vector3 respawnPoint;

    //Sound
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource jumpsound;
    [SerializeField] private AudioSource Dashsound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfjumpLeft = amountOfJump;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        respawnPoint = transform.position;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfcanJump();
        CheckDash();
        CheckifWallSliding();
        CheckJump();
        CheckKnockback();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    //Kiểm tra trượt tường 
    private void CheckifWallSliding()
    {
        if (isTouchingWall && movemenInputDirection == facingDirection && rb.velocity.y < 0 )
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
   
    public bool GetDashStatus()
    {
        return isDashing;
    }
    // Hiệu ứng đẩy lùi nhân vật
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }
    // Kiểm tra điều kiện để đẩy lùi khi va chạm
    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    //Kiểm tra trạng thái nhảy của nhân vật
    private void CheckIfcanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfjumpLeft = amountOfJump;
        }
        if(isTouchingWall)
        {
            canWallJump = true;
        }
        if (amountOfjumpLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }

    }
    // Kiểm tra trạng thái xung quanh (Tường và Đất)
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatisGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatisGround);
    }
    // Hướng di chuyển của nhân vật
    private void CheckMovementDirection()
    {
        if (isFacingRight && movemenInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movemenInputDirection > 0)
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }

    }
    // Hoạt động của nhân vật
    private void UpdateAnimations()
    {
        anim.SetBool("IsRunning", IsRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);


    }
    // Đoạn mã kiểm tra người dùng nhập và thực hiện hành động
    private void CheckInput()
    {
        movemenInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded || (amountOfjumpLeft > 0 && isTouchingWall))
            {
                jumpsound.Play();
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }

        }
        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {

            if (!isGrounded && movemenInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }    
        if(!canMove)
        {
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0 )
            {
                canMove = true;
                canFlip = true;
            }
        }
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                Dashsound.Play();
                AttemptToDash();
            }
        }
    }
    // Thực hiện hành động dash
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagepool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    // Kiểm tra trạng thái Dash của nhân vật
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
        
                transform.Translate(Vector2.right * dashSpeed * Time.deltaTime);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagepool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
 
            }
        }


    }
    private void CheckJump()
    {
        if(jumpTimer > 0)
        {
            if(!isGrounded && isTouchingWall && movemenInputDirection !=0 && movemenInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        if(wallJumpTimer > 0)
        {
            if (hasWallJumped && movemenInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <=0)
            {
                hasWallJumped = false;

            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }
    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            amountOfjumpLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }
    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            isWallSliding = false;
            amountOfjumpLeft = amountOfJump;
            amountOfjumpLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movemenInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    } 
    private void ApplyMovement()
    {
        if (!isGrounded && !isWallSliding && movemenInputDirection == 0 && !knockback)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }   
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(speed * movemenInputDirection, rb.velocity.y);
        }    
     
 
        if(isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }    
        }    

    }

    public void DisableFlip()
    {
        canFlip = false;
    }
    public void EnableFlip()
    {
        canFlip = true;
    }
    private void Flip()
    {
        if(!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }    

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void FootStep()
    {
        footstep.Play();
    }


}
