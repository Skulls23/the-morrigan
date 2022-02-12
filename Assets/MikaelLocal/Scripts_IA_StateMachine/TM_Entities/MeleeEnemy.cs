﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{

    private StateMachine _stateMachine;

    public event Action<int> OnGatheredChanged;
    public Waypoint[] Waypoints;
    public int currentIndex { get; set; }

    public Transform Target { get; set; }

    public bool isAttacking;

    [Header("COMPONENTS")]
    public PlayerDetector PD;
    public FollowZone FZ;
    public MidRangeAttackDetector MRAD;

    [Header("GAME DESIGN")]
    public float UpdateFollowTime;

    //Value in seconds
    public float UpdateTryAttack;

    [Range(0, 1)]
    public float MidRangeAttackProcPercentage;
    [Range(0, 1)]
    public float MidRangeAttack1Percentage = 0;
    [Range(0, 1)]
    public float MidRangeAttack2Percentage = 0;

    public float SteeringSpeed;

    public bool isSteering;
    public bool isAddingMovement;

    public float RoamingSpeed;
    public float FollowPlayerSpeed;

    private Animator anim;


    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponentInChildren<Animator>();
        anim = animator;

        _stateMachine = new StateMachine();

        var wait = new WaitOnWaypoint(this);
        var moveToSelected = new MoveToSelectedWayPoint(this, navMeshAgent, animator);
        var search = new SearchForWaypoint(this, navMeshAgent);
        var follow = new FollowPlayer(this, navMeshAgent, animator, PD, MRAD);
        var midRangeAttack = new MidRangeAttack(this, navMeshAgent, animator);


        At(search, moveToSelected, HasTarget());
        At(wait, moveToSelected, FinishedWaiting());
        At(moveToSelected, wait, ReachedWaypoint());
        At(follow, search, () => FZ.PlayerInZone == false);
        At(follow, midRangeAttack, IsMRASelected());
        At(midRangeAttack, follow, MRAFinished());

        At(moveToSelected, follow, IsTargetable());
        At(wait, follow, IsTargetable());

        //_stateMachine.AddAnyTransition(follow, IsTargetable());
        _stateMachine.AddAnyTransition(midRangeAttack, IsMRASelected());


        Target = Waypoints[0].transform;
        _stateMachine.SetState(moveToSelected);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> FinishedWaiting() => () => wait.TimeWaited > wait.timeToWait;
        Func<bool> ReachedWaypoint() => () => Target != null 
                                              && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.transform.position.x, Target.transform.position.z)) < Waypoints[currentIndex].WaypointRange;
        Func<bool> IsTargetable() => () => PD.PlayerInRange
                                           && FZ.PlayerInZone == true;

        Func<bool> IsMRASelected() => () => follow.MRASelected == true;
        Func<bool> MRAFinished() => () => midRangeAttack.attackFinished;
    }

    private void Update() {
        _stateMachine.Tick();

        /*if (_stateMachine.GetCurrentState().ToString() == "FollowPlayer")
        {
            Debug.Log((_stateMachine.GetCurrentState() as FollowPlayer).MRASelected);
        }*/
    }

    public Transform GetNextDestination()
    {
        currentIndex++;
        if (currentIndex >= Waypoints.Length)
            currentIndex = 0;

        return Waypoints[currentIndex].GetComponent<Transform>();
    }

    public void AttackHasFinished()
    {
        if(_stateMachine.GetCurrentState().ToString() == "MidRangeAttack")
        {
            (_stateMachine.GetCurrentState() as MidRangeAttack).attackFinished = true;
        }
        
    }

    public void OnAnimatorMove()
    {
         transform.position += anim.deltaPosition;

        //STEERING ATTACK
        if (isSteering)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = Target.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = SteeringSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void LateUpdate()
    {
        /*if (isAddingMovement)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * speed);
        }*/
    }
}