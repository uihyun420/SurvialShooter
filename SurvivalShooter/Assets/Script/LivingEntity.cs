using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public bool isDead { get; set; }
    public float Health { get; set; }

    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        isDead = false;
        Health = maxHealth;
    }
    public virtual void Ondamage(float damage, Vector3 hiPoint, Vector3 hitNormal)
    {
        Debug.Log($"{gameObject.name} Ondamage »£√‚µ , damage: {damage}");
        Health -= damage;
        if(Health <= 0)
        {
            Health = 0;
            Die();
            Destroy(gameObject, 2f);           
        }
    }

    public virtual void Die()
    {
        if(OnDeath!=null)
        {
            OnDeath();
        }
        isDead = true;
    }
}
