using UnityEngine;
using UnityEngine.AI;

internal class Dead : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;

    public Dead(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator)
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

        Debug.Log("Enter Dead");
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        animator.SetTrigger("die");
    }

    public void OnExit()
    {
        Debug.Log("Exit Dead");
    }
}
