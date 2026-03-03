using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float _attackVelocityTimer;
    private bool _comboAttackQueued;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();


        if (triggerCalled)
            HandleStateExit();
    }
    private void HandleStateExit()
    {
        if (_comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            Character.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(Character.IdleState);
    }
    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            Character.SetVelocity(0, rb.linearVelocity.y);
    }
}
