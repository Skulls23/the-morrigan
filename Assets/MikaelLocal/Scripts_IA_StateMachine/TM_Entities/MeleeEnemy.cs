using System;
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


    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponentInChildren<Animator>();

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

        _stateMachine.AddAnyTransition(follow, IsTargetable());
        

        Target = Waypoints[0].transform;
        _stateMachine.SetState(moveToSelected);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> FinishedWaiting() => () => wait.TimeWaited > wait.timeToWait;
        Func<bool> ReachedWaypoint() => () => Target != null 
                                              && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.transform.position.x, Target.transform.position.z)) < Waypoints[currentIndex].WaypointRange;
        Func<bool> IsTargetable() => () => PD.PlayerInRange
                                           && FZ.PlayerInZone == true;

        Func<bool> IsMRASelected() => () => follow.MRASelected;
        Func<bool> MRAFinished() => () => midRangeAttack.attackFinished;
    }

    private void Update() {
        _stateMachine.Tick();
        //Debug.Log(_stateMachine.GetCurrentState().ToString());
    }

    /*public void TakeFromTarget()
    {
        if (Target.Take())
        {
            _gathered++;
            OnGatheredChanged?.Invoke(_gathered);
        }
    }*/

    public Transform GetNextDestination()
    {
        currentIndex++;
        if (currentIndex >= Waypoints.Length)
            currentIndex = 0;

        return Waypoints[currentIndex].GetComponent<Transform>();
    }


    /*public bool Take()
    {
        if (_gathered <= 0)
            return false;
        
        _gathered--;
        OnGatheredChanged?.Invoke(_gathered);
        return true;
    }

    public void DropAllResources()
    {
        if (_gathered > 0)
        {
            FindObjectOfType<WoodDropper>().Drop(_gathered, transform.position);
            _gathered = 0;
            OnGatheredChanged?.Invoke(_gathered);
        }
    }*/
}