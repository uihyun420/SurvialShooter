using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/Gundata")] 
public class GunData : ScriptableObject
{
    public AudioClip gunShot;
    public int AmmoRemain = 100000;
    public int damage = 30;
    public float timeBetFire = 0.12f;
}
