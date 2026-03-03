using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Character.MoveInput.x == 0 || Character.WallDetected) 
            stateMachine.ChangeState(Character.IdleState);

        Character.SetVelocity(Character.MoveInput.x * Character.moveSpeed, rb.linearVelocity.y);
    }
}
