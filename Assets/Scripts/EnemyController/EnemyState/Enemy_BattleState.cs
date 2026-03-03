using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float _lastTimeWasInBattle; // 가장 마지막에 전투한 시간을 저장
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        if (player == null)
            player = Enemy.GetPlayerRefrence();
    }

    public override void Update()
    {
        base.Update();

        if (Enemy.PlayerDetection()) // 플레이어를 발견하면 마지막 전투시간을 저장함
            UpdateBattleTimer();

        if (BattleTimeOver()) // 가장 마지막 저장된 시간이 BattleTimeDuration을 지나게 되면 다시 순찰로 돌아감
            stateMachine.ChangeState(Enemy.IdleState);

        if (IsPlayerInAttackRange() && Enemy.PlayerDetection())
            stateMachine.ChangeState(Enemy.AttackState);
        else
            Enemy.SetVelocity(Enemy.BattleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }
    private void UpdateBattleTimer() => _lastTimeWasInBattle = Time.time;
    private bool BattleTimeOver() => Time.time > _lastTimeWasInBattle + Enemy.BattleTimeDuration;
    private bool IsPlayerInAttackRange() => GetPlayerDistance() < Enemy.AttackDistance;
    private float GetPlayerDistance()
    {
        return player == null ? float.MaxValue : Mathf.Abs(player.position.x - Enemy.transform.position.x);
    }
    private int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > Enemy.transform.position.x ? 1 : -1;
    }
}
