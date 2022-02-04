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

    public float UpdateFollowTime;

    public PlayerDetector PD;

    public FollowZone FZ;


    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponentInChildren<Animator>();

        _stateMachine = new StateMachine();

        var wait = new WaitOnWaypoint(this);
        var moveToSelected = new MoveToSelectedWayPoint(this, navMeshAgent, animator);
        var search = new SearchForWaypoint(this, navMeshAgent);
        var follow = new FollowPlayer(this, navMeshAgent, animator, PD);


        At(search, moveToSelected, HasTarget());
        At(wait, moveToSelected, FinishedWaiting());
        At(moveToSelected, wait, ReachedWaypoint());
        At(follow, search, () => FZ.PlayerInZone == false);

        _stateMachine.AddAnyTransition(follow, HasReturnedToWaypoint());
        

        Target = Waypoints[0].transform;
        _stateMachine.SetState(moveToSelected);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> FinishedWaiting() => () => wait.TimeWaited > wait.timeToWait;
        Func<bool> ReachedWaypoint() => () => Target != null 
                                              && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.transform.position.x, Target.transform.position.z)) < Waypoints[currentIndex].WaypointRange;
        Func<bool> HasReturnedToWaypoint() => () => PD.PlayerInRange
                                                    && _stateMachine.GetCurrentState().ToString() == "WaitOnWaypoint";

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