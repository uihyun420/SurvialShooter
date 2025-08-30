using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZomHellephant : LivingEntity
{
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die
    }

    public ParticleSystem hitEffect;

    private NavMeshAgent agent;
    private Animator animator;
    private Collider hellephantCollider;
    private AudioSource audioSource;

    private Transform target;
    public float traceDistance;
    public float attackDistance;

    public float damage = 10f;
    public float lastAttackTime;
    public float attackInterval = 1f;

    private Status currnetStatus;
    public Status CurrentStatus
    {
        get { return currnetStatus; }
        set
        {
            var prevState = currnetStatus;
            currnetStatus = value;
            switch (currnetStatus)
            {
                case Status.Idle:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool("HasTarget", true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case Status.Die:
                    animator.SetTrigger("Die");
                    agent.isStopped = true;
                    hellephantCollider.enabled = false;
                    break;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hellephantCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (currnetStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
        if (isDead) return;
    }

    private void UpdateIdle()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) < traceDistance)
        {
            CurrentStatus = Status.Trace;
        }
        target = FindTarget(traceDistance);
    }

    private void UpdateTrace()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }

        if (target == null || Vector3.Distance(transform.position, target.position) > traceDistance)
        {
            CurrentStatus = Status.Idle;
            return;
        }
        agent.SetDestination(target.position);
    }

    private void UpdateAttack()
    {
        if ((target == null) || (target != null && Vector3.Distance(transform.position, target.position) > attackDistance))
        {
            CurrentStatus = Status.Trace;
            return;
        }
        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if (lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;
            var damagable = target.GetComponent<IDamageable>();
            if (damagable != null)
            {
                damagable.Ondamage(damage, transform.position, -transform.forward);
            }
        }
    }
    private void UpdateDie()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Die()
    {
        CurrentStatus = Status.Die; // 상태를 Die로 변경(애니메이션 트리거 포함)
        base.Die();
    }

    public LayerMask targetLayer;

    protected Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();

        return target.transform;
    }

    public override void Ondamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.Ondamage(damage, hitPoint, hitNormal);
        if (hitEffect != null)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.forward = hitNormal;
            hitEffect.Play();
        }
    }

}
