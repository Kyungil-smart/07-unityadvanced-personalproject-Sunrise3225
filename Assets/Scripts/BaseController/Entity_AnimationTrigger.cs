using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    private Entity _entity;
    private Entity_Combat _entityCombat;
    protected virtual void Awake()
    {
        _entity = GetComponentInParent<Entity>();
        _entityCombat = GetComponentInParent<Entity_Combat>();
    }
    private void CurrentStateTrigger()
    {
        _entity.CurrentStateAnimationTrigger();
    }
    // 공격이 들어가는 애니메이션 이벤트
    private void AttackTrigger()
    {
        _entityCombat.PerformAttack();
    }

    private void LongAttackTrigger()
    {
        _entityCombat.PerformLongAttack();
    }
}
