using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Character.MoveInput.x != 0)
            Character.SetVelocity(Character.MoveInput.x * (Character.moveSpeed * Character.InAirMoveMultiplier), rb.linearVelocity.y);

        if (Character.Input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(Character.JumpAttackState);
    }
}
