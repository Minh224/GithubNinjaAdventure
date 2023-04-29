using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performClosRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Entini entini, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entini, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entini.CheckGround();
        performClosRangeAction = entini.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entini.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        entini.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entini.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entini.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entini.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
