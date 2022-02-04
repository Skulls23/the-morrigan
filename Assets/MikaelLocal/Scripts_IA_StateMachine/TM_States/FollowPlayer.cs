using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;
    private readonly PlayerDetector playerDetector;

    private readonly float refreshTime;
    private float refreshTimer = 0;

    public FollowPlayer(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator, PlayerDetector _playerDetector)
    {
        meleeEnemy = _meleeEnemy;
        refreshTime = meleeEnemy.UpdateFollowTime;
        navMeshAgent = _navMeshAgent;
        animator = _animator;
        playerDetector = _playerDetector;
    }


    public void Tick()
    {
        if (refreshTimer > refreshTime)
        {
            refreshTimer = 0;
            RecalculatePlayerPos();
        }
        refreshTimer += Time.deltaTime;
    }

    private void RecalculatePlayerPos()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(meleeEnemy.Target.transform.position);
        animator.SetFloat("vertical", 1f);
    }

    public void OnEnter() {
        Debug.Log("Enter FollowPlayer");
        refreshTimer = 0;
        navMeshAgent.speed = 12;
        meleeEnemy.Target = playerDetector.GetPlayerTranform();
        RecalculatePlayerPos();
    }
    public void OnExit() {
        Debug.Log("Exit FollowPlayer");
    }


}
