using UnityEngine;
using UnityEngine.AI;

internal class GetHit : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;


    private static readonly int Speed = Animator.StringToHash("Speed");

    public bool actionFinished = false;

    public GetHit(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator)
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

        Debug.Log("Enter GetHit");
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        animator.SetTrigger("getHit");
        //meleeEnemy.GetComponent<Rigidbody>().useGravity = false;
        //animator.applyRootMotion = true;
        actionFinished = false;
    }

    public void OnExit()
    {
        Debug.Log("Exit GetHit");
        meleeEnemy.GetComponent<Enemy>().hasBeenHit = false;
        //meleeEnemy.GetComponent<Rigidbody>().useGravity = true;
        //animator.applyRootMotion = false;
        actionFinished = false;
        navMeshAgent.isStopped = false;
    }
}