using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

    public DeadState(Entini entini, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entini, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.dealthBloodParticle, entini.aliveGO.transform.position, stateData.dealthBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.dealthChunkParticle, entini.aliveGO.transform.position, stateData.dealthChunkParticle.transform.rotation);

        entini.gameObject.SetActive(false);

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
