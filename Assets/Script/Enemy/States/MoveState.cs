using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayeInMinAgroRange;

    public MoveState(Entini entini, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entini, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = entini.CheckLedge();
        isDetectingWall = entini.CheckWall();
        isPlayeInMinAgroRange = entini.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entini.SetVelocity(stateData.movementSpeed);


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}

