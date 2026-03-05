using UnityEngine;

public class Enemy_BossDeadState : EnemyState
{
    public Enemy_BossDeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.simulated = false;
    }
}
