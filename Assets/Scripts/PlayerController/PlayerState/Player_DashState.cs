
public class Player_DashState : PlayerState
{
    private float _originalGravityScale;
    private int _dashDir;
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _dashDir = Character.MoveInput.x != 0 ? ((int)Character.MoveInput.x) : Character.FacingDir;
        stateTimer = Character.DashDuration;

        _originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }
    public override void Update()
    {
        base.Update();

        TryCancelDash();
        Character.SetVelocity(Character.DashSpeed * _dashDir, 0);

        if (stateTimer < 0)
        {
            if (Character.GroundDetected)
                stateMachine.ChangeState(Character.IdleState);
            else
                stateMachine.ChangeState(Character.FallState);
        }
    }
    public override void Exit()
    {
        base.Exit();

        Character.SetVelocity(0, 0);
        rb.gravityScale = _originalGravityScale;
    }

    void TryCancelDash()
    {
        if (Character.WallDetected)
        {
            if (Character.GroundDetected)
                stateMachine.ChangeState(Character.IdleState);
        }
    }
}
