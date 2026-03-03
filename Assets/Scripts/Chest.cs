using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    private Animator _anim => GetComponentInChildren<Animator>();
    private Rigidbody2D _rb => GetComponentInChildren<Rigidbody2D>();
    private Entity_VFX _fx => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 openVelocity;
    [SerializeField] private int damageCount = 2;

    private bool _isOpen = false;
    public void TakeDamage(float damage, Transform damageDealer)
    {
        if (_isOpen) return;

        damageCount--;
        _fx.PlayOnDamageVfx();
        _rb.linearVelocity = openVelocity;
        _rb.angularVelocity = Random.Range(-200f, 200f);

        if (damageCount <= 0)
            Broken();
    }
    private void Broken()
    {
        _isOpen = true;
        _anim.SetBool("ChestOpen", true);
    }
}
