using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public bool isDead { get; set; }
    public float Health { get; set; }

    public event Action OnDeath;

    private void OnEnable()
    {
        isDead = false;
        Health = maxHealth;
    }
    public void Ondamage(float damage, Vector3 hiPoint, Vector3 hitNormal)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public void Die()
    {
        if(OnDeath!=null)
        {
            OnDeath();
        }
        isDead = true;
    }
}
