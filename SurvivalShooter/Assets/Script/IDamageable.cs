using UnityEngine;

public interface IDamageable 
{
    void Ondamage(float damage, Vector3 hiPoint, Vector3 hitNormal);
}
