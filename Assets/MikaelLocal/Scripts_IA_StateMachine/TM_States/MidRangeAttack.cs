using UnityEngine;
using UnityEngine.AI;

internal class MidRangeAttack : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 lastPosition = Vector3.zero;

    public float TimeStuck;

    public MidRangeAttack(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator)
    {
        meleeEnemy = _meleeEnemy;
        navMeshAgent = _navMeshAgent;
        animator = _animator;
    }

    public void Tick()
    {
        //AttackSteering
    }

    public void OnEnter()
    {
        Debug.Log("Enter MoveToSelectedWayPoint");

        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(meleeEnemy.Target.transform.position);
        navMeshAgent.speed = 2;

        float rand = Random.Range(0, 1);
        if(rand > 0)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnExit()
    {
        Debug.Log("Exit MoveToSelectedWayPoint");
        //navMeshAgent.enabled = false;
        animator.SetFloat("vertical", 0f);
    }
}
