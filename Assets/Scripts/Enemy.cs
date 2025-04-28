using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent NavAgent;
    public bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            isDead = true;
            int random = Random.Range(0, 2); //0 or 1
            if (random == 0)
            {
                animator.SetTrigger("DieBack");
            }
            else
            {
                animator.SetTrigger("DieForward");
            }

            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieHurt);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);//attacking // stop attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15f);//detection for chasing

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 18f);//stop chasing
    }
}