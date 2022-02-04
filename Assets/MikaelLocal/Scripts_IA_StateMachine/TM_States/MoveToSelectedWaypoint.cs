using UnityEngine;
using UnityEngine.AI;

internal class MoveToSelectedWayPoint : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 lastPosition = Vector3.zero;
    
    public float TimeStuck;

    public MoveToSelectedWayPoint(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator)
    {
        meleeEnemy = _meleeEnemy;
        navMeshAgent = _navMeshAgent;
        animator = _animator;
    }
    
    public void Tick()
    {
        /*if (Vector3.Distance(meleeEnemy.transform.position, lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        lastPosition = meleeEnemy.transform.position;*/
    }

    public void OnEnter()
    {
        //TimeStuck = 0f;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(meleeEnemy.Target.transform.position);
        animator.SetFloat("vertical", 1f);
    }

    public void OnExit()
    {
        //navMeshAgent.enabled = false;
        animator.SetFloat("vertical", 0f);
    }
}