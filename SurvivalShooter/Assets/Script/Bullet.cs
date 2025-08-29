using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFire = false;
    public float speed = 12f;
    Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if(isFire)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
    }

}
