using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public static readonly int IdDie = Animator.StringToHash("Die");
    private Animator animator;
    private PlayerInput playerInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerInput.enabled = true;
    }

    public override void Ondamage(float damage, Vector3 hiPoint, Vector3 hitNormal)
    {
        if(isDead)
        {
            return;
        }
        base.Ondamage(damage, hiPoint, hitNormal);
    }
    public override void Die()
    {
        base.Die();
        animator.SetTrigger(IdDie);
        playerInput.enabled = false;
        //GetComponent<CharacterController>().enabled = false;
    }
}
