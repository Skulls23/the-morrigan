using UnityEngine;
using UnityEngine.AI;
using System.Collections;

internal class MidRangeAttack : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;


    private static readonly int Speed = Animator.StringToHash("Speed");

    private IEnumerator currentCoroutine;

    public bool attackFinished = false;


    private float MRA1P;
    private float MRA2P;

    public MidRangeAttack(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator)
    {
        meleeEnemy = _meleeEnemy;
        navMeshAgent = _navMeshAgent;
        animator = _animator;


        MRA1P = meleeEnemy.MidRangeAttack1Percentage;
        MRA2P = meleeEnemy.MidRangeAttack2Percentage;
    }

    public void Tick()
    {
        //AttackSteering
    }

    public void OnEnter()
    {
        Debug.Log("Enter MidRangeAttack");
        attackFinished = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        ChooseAttack();
    }

    public void OnExit()
    {
        navMeshAgent.isStopped = false;
        Debug.Log("Exit MidRangeAttack");
        
    }

    private void ChooseAttack()
    {
        
        float rand = Random.Range(0, 1);

        if (rand < MRA1P)
        {
            navMeshAgent.enabled = false;
            animator.SetTrigger("attack1");
        }
        else if (rand < (1-MRA2P))
        {
            navMeshAgent.enabled = false;
            animator.SetTrigger("attack2");
        }
    }

    public void attackHasFinished()
    {
        attackFinished = true;
    }
}
