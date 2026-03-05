
public class Enemy_Boss : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, StateMachine, "Idle");
        MoveState = new Enemy_MoveState(this, StateMachine, "Move");
        AttackState = new Enemy_AttackState(this, StateMachine, "Attack");
        BattleState = new Enemy_BattleState(this, StateMachine, "Battle");
        BossDeadState = new Enemy_BossDeadState(this, StateMachine, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
    public override void EntityDead()
    {
        base.EntityDead();
        StateMachine.ChangeState(BossDeadState);
    }
}
