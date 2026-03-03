using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        Character.SetVelocity(rb.linearVelocity.x, Character.jumpForce);
    }
    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && stateMachine.CurrentState != Character.JumpAttackState) 
            stateMachine.ChangeState(Character.FallState);
    }
}
