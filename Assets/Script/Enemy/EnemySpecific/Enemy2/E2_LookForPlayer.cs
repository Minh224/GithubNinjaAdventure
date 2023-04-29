using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayer : LookForPlayerState
{
    private Enemy2 enemy;
    public E2_LookForPlayer(Entini entini, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Enemy2 enemy) : base(entini, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
