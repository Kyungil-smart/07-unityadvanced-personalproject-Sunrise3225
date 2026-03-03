
public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();

        if (!Character.GroundDetected)
        {
            stateMachine.ChangeState(Character.FallState);
            return;
        }

        if (Character.Input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(Character.JumpState);

        if (Character.Input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(Character.BasicAttackState);
    }
}
