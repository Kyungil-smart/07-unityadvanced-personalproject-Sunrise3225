using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static event Action OnPlayerDeath;
    #region Player State
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }
    public Player_JumpAttackState JumpAttackState { get; private set; }
    public Player_DeadState DeadState { get; private set; }
    #endregion
    public PlayerInputSet Input { get; private set; }

    [Header("Attack Details")]
    public Vector2[] AttackVelocity;
    public Vector2 JumpAttackVelocity;
    public float AttackVelocityDuration = 0.05f;
    private Coroutine _queueAttackCo;

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    [Range(0, 1)]
    public float InAirMoveMultiplier = 0.7f; // 이 값은 0 ~ 1사이 여야한다.
    [Space]
    public float DashDuration = 0.25f;
    public float DashSpeed = 20f;

    public Vector2 MoveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Input = new PlayerInputSet();

        // Animator 의 이름과 동일해야함 (여기선 Bool 파라미터)
        IdleState = new Player_IdleState(this, StateMachine, "Idle");
        MoveState = new Player_MoveState(this, StateMachine, "Move");
        JumpState = new Player_JumpState(this, StateMachine, "JumpFall");
        FallState = new Player_FallState(this, StateMachine, "JumpFall");
        DashState = new Player_DashState(this, StateMachine, "Dash");
        BasicAttackState = new Player_BasicAttackState(this, StateMachine, "BasicAttack");
        JumpAttackState = new Player_JumpAttackState(this, StateMachine, "JumpAttack");
        DeadState = new Player_DeadState(this, StateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
    private void OnEnable()
    {
        Input.Enable();

        Input.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        Input.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        Input.Disable();
    }
    public override void EntityDead()
    {
        base.EntityDead();
        OnPlayerDeath?.Invoke();
        StateMachine.ChangeState(DeadState);
    }
    public void EnterAttackStateWithDelay()
    {
        if (_queueAttackCo != null)
            StopCoroutine(_queueAttackCo);

        _queueAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        StateMachine.ChangeState(BasicAttackState);
    }
}
