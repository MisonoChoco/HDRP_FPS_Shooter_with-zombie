using UnityEngine;

public class TurretFSM : MonoBehaviour
{
    private enum State
    { Idle, Attack } // FSM States

    private State currentState;

    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float detectionRange = 5f;
    public float fireRate = 1f;
    public float rotationSpeed = 100f;
    public float bulletSpeed = 10f;

    private float nextFireTime;

    private void Start()
    {
        currentState = State.Idle; // Start with Idle state
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // FSM State Transitions
        if (distance <= detectionRange)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Idle;
        }

        // Execute State Behavior
        switch (currentState)
        {
            case State.Idle:
                Spin();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    private void Spin()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // Face the player

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}