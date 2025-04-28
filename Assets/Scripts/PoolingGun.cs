using UnityEngine;

public class PoolingGun : MonoBehaviour
{
    public Transform firePoint;
    private ObjectPool pool;

    private void Start()
    {
        pool = Object.FindFirstObjectByType<ObjectPool>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = pool.GetObject(firePoint.position, firePoint.rotation);
    }
}