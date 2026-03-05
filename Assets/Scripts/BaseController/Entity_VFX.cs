using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity _entity;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;  // 하얗게 변하는 메테리얼 적용
    [SerializeField] private float onDamageVfxDuration = 0.2f; // 맞았을 때 하얗게 변하는 시간
    private Material originalMaterial;
    private Coroutine onDamageVfxCo;

    [Header("Create VFX")]
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private float destroyVfxDelay = 1.5f;

    [Header("Random rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;

    [Header("Random Position")]
    [SerializeField] private bool randomVfx = true;
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [Space]
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;


    private void Awake()
    {
        _entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCo != null)
            StopCoroutine(onDamageVfxCo);

        onDamageVfxCo = StartCoroutine(OnDamageVfxCo());
    }
    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
        onDamageVfxCo = null;
    }
    public void CreateOnHitVFX(Transform target)
    {
        StartCoroutine(AutoDestroyVfxCo(target));
    }
    private IEnumerator AutoDestroyVfxCo(Transform target)
    {
        Vector3 vfxPosition = target.position;
        Quaternion vfxRotation = Quaternion.identity;

        if (randomVfx)
        {
            float xOffset = Random.Range(xMinOffset, xMaxOffset);
            float yOffset = Random.Range(yMinOffset, yMaxOffset);
            float zRotation = Random.Range(minRotation, maxRotation);

            vfxPosition += new Vector3(xOffset, yOffset, 0f);
            vfxRotation = Quaternion.Euler(0, 0, zRotation);
        }

        GameObject vfx = Instantiate(hitVfx, vfxPosition, vfxRotation);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

        if (_entity.FacingDir == -1)
            vfx.transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(destroyVfxDelay);

        Destroy(vfx);
    }
}
