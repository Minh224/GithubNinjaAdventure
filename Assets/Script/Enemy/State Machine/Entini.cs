using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entini : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entini entiniData;

    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }





    public int lastDamageDirection { get; private set; }
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;


    private float currenHealth;
    private float currentStunResistance;
    private float lastDamageTime;



    private Vector2 velocityWorkspace;

    protected bool isStuned;
    protected bool isDead;



    public virtual void Start()
    {
        facingDirection = 1;
        currenHealth = entiniData.maxHealth;
        currentStunResistance = entiniData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        stateMachine = new FiniteStateMachine();

    }
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", rb.velocity.y);

        if (Time.time >= lastDamageTime + entiniData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;

    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entiniData.wallCheckDistance, entiniData.whatIsGround);

    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entiniData.ledgeCheckDistance, entiniData.whatIsGround);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entiniData.groundCheckRadius, entiniData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entiniData.minAgroDistance, entiniData.whatIsPlayer);

    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entiniData.maxAgroDistance, entiniData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entiniData.closeRangeActionDistance, entiniData.whatIsPlayer);
    }
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }
    public virtual void ResetStunResistance()
    {
        isStuned = false;
        currentStunResistance = entiniData.stunResistance;
    }


    public virtual void Damage(AttackDetails attackDetails )
    {
        lastDamageTime = Time.time;

        currenHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;
        //currenHealth -= attackDetails.gunDamageAmount; // Giảm điểm máu của Entini khi bị bắn

        DamageHop(entiniData.damageHopSpeed);

        Instantiate(entiniData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStuned = true;
        }
        if (currenHealth <= 0)
        {
            isDead = true;
        }
    }




    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entiniData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entiniData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(transform.right * entiniData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(transform.right * entiniData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(transform.right * entiniData.maxAgroDistance), 0.2f);

    }
}
