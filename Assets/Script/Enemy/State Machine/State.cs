using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entini entini;
    public float startTime { get; protected set; }

    protected string animBoolName;
    public State(Entini entini, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entini = entini;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

    }
    public virtual void Enter()
    {
        startTime = Time.time;
        entini.anim.SetBool(animBoolName, true);
        DoChecks();

    }
    public virtual void Exit()
    {
        entini.anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
