using System;
using UnityEngine;

public class Enemy : Entity
{
    #region Enemy State
    public Enemy_IdleState IdleState;
    public Enemy_MoveState MoveState;
    public Enemy_AttackState AttackState;
    public Enemy_BattleState BattleState;
    public Enemy_DeadState DeadState;
    public Enemy_BossDeadState BossDeadState;
    #endregion

    [Header("Battle details")]
    public float BattleMoveSpeed = 3f;
    public float AttackDistance = 2f;
    public float BattleTimeDuration = 5f;

    [Header("Movement details")]
    public float IdleTime = 2f;
    public float MoveSpeed = 3f;
    [Range (0, 2)]
    public float MoveAnimSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;

    public Transform _player { get; private set; }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }
    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    public override void EntityDead()
    {
        base.EntityDead();
    }
    private void HandlePlayerDeath()
    {
        StateMachine.ChangeState(IdleState);
    }
    public void TryEnterBattleState(Transform player)
    {
        if (StateMachine.CurrentState == BattleState || StateMachine.CurrentState == AttackState)
            return;

        this._player = player;
        StateMachine.ChangeState(BattleState);
    }
    public Transform GetPlayerRefrence()
    {
        if (_player == null)
            _player = PlayerDetection().transform;

        return _player;
    }
    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D hit = 
            Physics2D.Raycast(playerCheck.position, Vector2.right * FacingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (FacingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (FacingDir * AttackDistance), playerCheck.position.y));
      
    }
}
