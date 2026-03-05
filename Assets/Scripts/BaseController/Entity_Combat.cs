using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX _vfx;

    public float damage = 30f;

    [Header("Target detections")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIstarget;

    [Header("Long Range Target detections")]
    [SerializeField] private Transform targetCheck_02;
    [SerializeField] private float targetCheckRadius_02 = 1;


    private void Awake()
    {
        _vfx = GetComponent<Entity_VFX>();
    }
    public void PerformAttack() // 애니메이션 이벤트용 함수 (공격이 들어가는 순간 호출한다)
    {
        AttackCheck(targetCheck, targetCheckRadius);
    }

    public void PerformLongAttack() // 애니메이션 이벤트용 함수 (공격이 들어가는 순간 호출한다)
    {
        Transform check = targetCheck_02 != null ? targetCheck_02 : targetCheck;
        float radius = targetCheck_02 != null ? targetCheckRadius_02 : targetCheckRadius;

        AttackCheck(check, radius);
    }

    private void AttackCheck(Transform checkPoint, float radius)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(checkPoint.position, radius, whatIstarget);

        foreach (Collider2D target in targets)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage, transform);
            _vfx.CreateOnHitVFX(target.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (targetCheck != null)
            Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);

        if (targetCheck_02 != null)
            Gizmos.DrawWireSphere(targetCheck_02.position, targetCheckRadius_02);
    }
}
