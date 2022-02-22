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
    public Collider AttackCollider;

    [Header("GAME DESIGN")]
    public float UpdateFollowTime;

    //Value in seconds
    public float UpdateTryAttack;

    public float Multiplicator;

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

    public bool isRootMotionEnemy = true;
    public bool canBeCanceled = false;

    public Transform spawnPoint;
    public GameObject ProjectilePrefab;
    public float projectileSpeed;

    private Animator anim;

    private void Awake()
    {
        var enemy = GetComponent<Enemy>();
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponentInChildren<Animator>();
        anim = animator;

        _stateMachine = new StateMachine();

        var wait = new WaitOnWaypoint(this);
        var moveToSelected = new MoveToSelectedWayPoint(this, navMeshAgent, animator);
        var search = new SearchForWaypoint(this, navMeshAgent);
        var follow = new FollowPlayer(this, navMeshAgent, animator, PD, MRAD);
        var midRangeAttack = new MidRangeAttack(this, navMeshAgent, animator);
        var hit = new GetHit(this, navMeshAgent, animator);
        var dead = new Dead(this, navMeshAgent, animator);

        At(search, moveToSelected, HasTarget());
        At(wait, moveToSelected, FinishedWaiting());
        At(moveToSelected, wait, ReachedWaypoint());
        At(follow, search, () => FZ.PlayerInZone == false);
        At(follow, midRangeAttack, IsMRASelected());
        At(midRangeAttack, follow, MRAFinished());
        At(follow, hit, HasBeenHit());

        if (canBeCanceled)
        {
            At(midRangeAttack, hit, HasBeenHit());
        }

        At(moveToSelected, follow, IsTargetable());
        At(wait, follow, IsTargetable());
        At(hit, follow, AnimationFinished());
        //At(hit, dead, IsDead());


        //_stateMachine.AddAnyTransition(follow, IsTargetable());
        //_stateMachine.AddAnyTransition(midRangeAttack, IsMRASelected());
        //_stateMachine.AddAnyTransition(hit, HasBeenHit());
        _stateMachine.AddAnyTransition(dead, IsDead());

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

        Func<bool> HasBeenHit() => () => enemy.hasBeenHit == true;
        Func<bool> AnimationFinished() => () => hit.actionFinished == true;

        Func<bool> IsDead() => () => enemy.IsDead;
    }

    private void Update() {
        _stateMachine.Tick();

        //Debug.Log(_stateMachine.GetCurrentState().ToString());

        /*if (_stateMachine.GetCurrentState().ToString() == "GetHit")
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
        if (_stateMachine.GetCurrentState().ToString() == "GetHit")
        {
            (_stateMachine.GetCurrentState() as GetHit).actionFinished = true;
        }

    }

    public void OnAnimatorMove()
    {
        // transform.position += new Vector3(anim.deltaPosition.x * Multiplicator, 0, anim.deltaPosition.z * Multiplicator);
        transform.position += anim.deltaPosition;
        if (isAddingMovement)
        {
            transform.position += transform.forward * Multiplicator * Time.deltaTime;
        }
        //new Vector3(Time.deltaTime * Multiplicator, 0, Time.deltaTime * Multiplicator);

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

    public void SetIsAttacking(bool value)
    {
        isAttacking = value;
        if (isAttacking)
        {
            AttackCollider.gameObject.SetActive(true);
        }
        else
        {
            AttackCollider.gameObject.SetActive(false);
        }
    }

    public void RangeAttack()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (Target.transform.position + new Vector3(0, 2, 0) - spawnPoint.transform.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity *= projectileSpeed;
    }

    public void LateUpdate()
    {
        /*if (isAddingMovement)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * speed);
        }*/
    }
}