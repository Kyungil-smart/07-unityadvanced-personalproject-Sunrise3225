
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, StateMachine, "Idle");
        MoveState = new Enemy_MoveState(this, StateMachine, "Move");
        AttackState = new Enemy_AttackState(this, StateMachine, "Attack");
        BattleState = new Enemy_BattleState(this, StateMachine, "Battle");
        DeadState = new Enemy_DeadState(this, StateMachine, "Idle");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
}
