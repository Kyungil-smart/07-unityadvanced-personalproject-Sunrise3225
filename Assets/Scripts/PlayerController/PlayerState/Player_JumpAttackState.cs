using UnityEngine;

public class Player_JumpAttackState : PlayerState
{
    private bool _touchGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _touchGround = false;

        float xVelocity = Character.MoveInput.x != 0 ? Character.JumpAttackVelocity.x * Character.FacingDir : 0f;
        Character.SetVelocity(xVelocity, Character.JumpAttackVelocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (Character.GroundDetected && !_touchGround)
        {
            _touchGround = true;
            anim.SetTrigger("JumpAttackTrigger");
            Character.SetVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && Character.GroundDetected)
            stateMachine.ChangeState(Character.IdleState);
    }
}
