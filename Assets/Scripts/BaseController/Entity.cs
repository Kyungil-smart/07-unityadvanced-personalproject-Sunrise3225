using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator Anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    protected StateMachine StateMachine;

    private bool _facingRight = true;
    public int FacingDir { get; private set; } = 1;
    
    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }

    private bool _isKnockback;
    private Coroutine _knockbackCo;

    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>(); // Awake 순서가 중요하다 Anim을 먼저 초기화 해줘야 밑의 State 부분들이 적용됨
        rb = GetComponent<Rigidbody2D>();
        StateMachine = new StateMachine();
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        StateMachine.UpdateActiveState();
    }

    public virtual void EntityDead() { }
    public void CurrentStateAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }
    public void ReciveKnockback(Vector2 knockback, float duration)
    {
        if (_knockbackCo != null)
            StopCoroutine(_knockbackCo);

        _knockbackCo = StartCoroutine(KnockbackCo(knockback, duration));
    }
    private IEnumerator KnockbackCo(Vector2 knockback, float duration)
    {
        _isKnockback = true;
        rb.linearVelocity = knockback;

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero; // 해당 값을 넣어주지 않으면 넉백의 힘으로 계속 미끄러진다.
        _isKnockback = false;
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (_isKnockback) return; // 넉백중에는 이동이 안되게 설정

        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && _facingRight == false)
            Flip();
        else if (xVelocity < 0 && _facingRight)
            Flip();
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        _facingRight = !_facingRight;
        FacingDir = FacingDir * -1;
    }
    void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        WallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * FacingDir, wallCheckDistance, whatIsGround);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * FacingDir, 0));
    }
}
