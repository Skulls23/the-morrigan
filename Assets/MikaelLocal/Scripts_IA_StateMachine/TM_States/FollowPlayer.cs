using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;
    private readonly PlayerDetector playerDetector;

    private readonly MidRangeAttackDetector midRangeDetector;
    private readonly PlayerDetector longRangeDetector;

    private readonly float refreshTime;
    private float refreshTimer = 0;

    private float MRATryAttackTime;
    private float MRAProcPercentage;
    private float MRATryAttackTimer = 0;

    public bool MRASelected;


    public FollowPlayer(MeleeEnemy _meleeEnemy, NavMeshAgent _navMeshAgent, Animator _animator, PlayerDetector _playerDetector, MidRangeAttackDetector _midRangeDetector)
    {
        meleeEnemy = _meleeEnemy;
        refreshTime = meleeEnemy.UpdateFollowTime;
        navMeshAgent = _navMeshAgent;
        animator = _animator;
        playerDetector = _playerDetector;
        midRangeDetector = _midRangeDetector;
        
        MRAProcPercentage = meleeEnemy.MidRangeAttackProcPercentage;
        MRATryAttackTime = meleeEnemy.UpdateTryAttack;
    }


    public void Tick()
    {
        if (refreshTimer > refreshTime)
        {
            refreshTimer = 0;
            RecalculatePlayerPos();
        }
        refreshTimer += Time.deltaTime;

        if (midRangeDetector.PlayerInRange)
        {
            if(MRATryAttackTimer > MRATryAttackTime)
            {
                float rand = Random.Range(0, 1);
                if(rand < MRAProcPercentage)
                {
                    MRASelected = true;
                }
            }
        }
    }

    private void RecalculatePlayerPos()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(meleeEnemy.Target.transform.position);
        animator.SetFloat("vertical", 1f);
    }

    public void OnEnter() {
        Debug.Log("Enter FollowPlayer");

        //PLAYER FOLLOW
        refreshTimer = 0;
        navMeshAgent.speed = 4.5f;
        meleeEnemy.Target = playerDetector.GetPlayerTranform();
        RecalculatePlayerPos();

        //ATTACKS
        MRATryAttackTimer = 0;
    }
    public void OnExit() {
        Debug.Log("Exit FollowPlayer");
        MRASelected = false;
    }


}
