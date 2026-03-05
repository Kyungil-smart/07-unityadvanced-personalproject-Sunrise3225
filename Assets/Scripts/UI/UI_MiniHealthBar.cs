using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();
    }
    private void OnEnable()
    {
        _entity.OnFlipped += HandleFlip;
        _entity.OnEnemyDead += HandleEnemyDead;
    }
    private void OnDisable()
    {
        _entity.OnFlipped -= HandleFlip;
        _entity.OnEnemyDead -= HandleEnemyDead;
    }
    private void HandleFlip() => transform.rotation = Quaternion.identity;
    private void HandleEnemyDead() => gameObject.SetActive(false);
}
