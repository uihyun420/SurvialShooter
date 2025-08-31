using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public static readonly int IdDie = Animator.StringToHash("Die");
    private Animator animator;
    private PlayerInput playerInput;
    private AudioSource audioSource;

    public AudioClip deathClip;
    public AudioClip hurtClip;

    public Ui ui;
    public Slider hpBar;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(hurtClip);
        ui.HitEffect();
        hpBar.value = Health / maxHealth;
    }
    public override void Die()
    {
        base.Die();
        hpBar.gameObject.SetActive(false);
        animator.SetTrigger(IdDie);
        playerInput.enabled = false;
        audioSource.PlayOneShot(deathClip);
        ui.GameOver();
        //GetComponent<CharacterController>().enabled = false;
    }

}
