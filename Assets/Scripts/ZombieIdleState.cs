using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    private float timer;
    public float idleTime = 0f;
    private Transform Player;
    public float detectionAreaRadius = 18f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //to patrol state
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        //to chase state
        float distanceFromPlayer = Vector3.Distance(Player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }
}