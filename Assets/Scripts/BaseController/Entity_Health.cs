using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamagable
{
    private Entity_VFX _entityVfx;
    private Entity _entity;

    [SerializeField] protected float CurrentHp;
    [SerializeField] protected float MaxHp = 100;
    [SerializeField] protected bool IsDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 _knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 _heavyKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] private float _knockbackDuration = 0.2f;
    [SerializeField] private float _heavyKnockbackDuration = 0.5f;
    [Header("On Heavy Damage")]
    [SerializeField] private float _heavyDamageThreshold = 0.3f; 

    protected virtual void Awake()
    {
        _entityVfx = GetComponent<Entity_VFX>();
        _entity = GetComponent<Entity>();
        CurrentHp = MaxHp;
    }
    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (IsDead) return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        _entity?.ReciveKnockback(knockback, duration);
        _entityVfx?.PlayOnDamageVfx();

        ReduceHp(damage);
    }
    protected void ReduceHp(float damage)
    {
        CurrentHp -= damage;

        if (CurrentHp <= 0)
            Die();
    }

    private void Die()
    {
        IsDead = true;
        _entity.EntityDead();
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? _heavyKnockbackPower : _knockbackPower;
        knockback.x *= direction;

        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? _heavyKnockbackDuration : _knockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / MaxHp >= _heavyDamageThreshold;
}
