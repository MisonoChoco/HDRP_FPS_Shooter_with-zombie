using UnityEngine;

public class PoolingBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f; // Bullet disappears after 3 seconds

    private ObjectPool pool;

    private void Start()
    {
        pool = Object.FindFirstObjectByType<ObjectPool>();
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage or effects here...
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }
}