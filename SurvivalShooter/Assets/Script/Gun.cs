using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Jobs;

public class Gun : MonoBehaviour
{
    public Transform firePosition;
    public GunData gunData;

    private AudioSource audioSource;

    public ParticleSystem muzzleEffect;

    private LineRenderer lineRenderer;


    public float fireLastTime;
    private float fireDistance = 100f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;

    }

    private void OnEnable()
    {
        fireLastTime = 0f;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (Time.time > fireLastTime + gunData.timeBetFire)
        {
            fireLastTime = Time.time;
            Shot();
            audioSource.PlayOneShot(gunData.gunShot);
        }
    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        if (muzzleEffect != null)
            muzzleEffect.Play();

        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePosition.position);
            lineRenderer.SetPosition(1, hitPosition);
        }

        // 0.03초 동안 이펙트 유지
        yield return new WaitForSeconds(0.03f);

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    private void Shot()
    {
        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.Ondamage(gunData.damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }

        hitPosition = firePosition.position + firePosition.forward * fireDistance;
        StartCoroutine(CoShotEffect(hitPosition));
    }
}
