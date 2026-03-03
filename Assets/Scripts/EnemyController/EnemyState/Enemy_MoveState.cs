
public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (!Enemy.GroundDetected || Enemy.WallDetected)
            Enemy.Flip();
    }
    public override void Update()
    {
        base.Update();

        Enemy.SetVelocity(Enemy.MoveSpeed * Enemy.FacingDir, rb.linearVelocity.y);

        if (!Enemy.GroundDetected || Enemy.WallDetected)
            stateMachine.ChangeState(Enemy.IdleState);
    }
}
