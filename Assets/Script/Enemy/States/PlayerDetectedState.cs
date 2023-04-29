using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entini entini, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entini, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entini.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entini.CheckPlayerInMaxAgroRange();
        isDetectingLedge = entini.CheckLedge();
        performCloseRangeAction = entini.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        entini.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeAcTionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
