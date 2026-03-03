using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Character.SetVelocity(0, rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (Character.MoveInput.x == Character.FacingDir && Character.WallDetected)
            return;

        if (Character.MoveInput.x != 0)
            stateMachine.ChangeState(Character.MoveState);
    }
}
