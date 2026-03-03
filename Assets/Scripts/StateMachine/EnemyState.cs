
public class EnemyState : EntityState
{
    protected Enemy Enemy;
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.Enemy = enemy;

        rb = Enemy.rb;
        anim = Enemy.Anim;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = Enemy.BattleMoveSpeed / Enemy.MoveSpeed;

        anim.SetFloat("BattleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        anim.SetFloat("MoveAnimSpeedMultiplier", Enemy.MoveAnimSpeedMultiplier);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
