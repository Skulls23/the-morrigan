using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SearchForWaypoint : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private readonly NavMeshAgent navMeshAgent;

    private NavMeshPath path = null;

    public SearchForWaypoint(MeleeEnemy _meleeEnemy,NavMeshAgent _navMeshAgent)
    {
        meleeEnemy = _meleeEnemy;
        navMeshAgent = _navMeshAgent;
    }
    public void Tick()
    {
        meleeEnemy.Target = ChooseNearestWaypoint();
    }

    private Transform ChooseNearestWaypoint()
    {
        NavMeshAgent tempAgent = navMeshAgent;
        Transform tempWP = meleeEnemy.Waypoints[0].transform;
        
        float distance = -1;


        foreach (Waypoint wP in meleeEnemy.Waypoints)
        {
            tempAgent.destination = wP.transform.position;
            tempAgent.CalculatePath(wP.transform.position, path);
            if(path.status == NavMeshPathStatus.PathComplete)
            {
                if(distance == -1 || tempAgent.remainingDistance <= distance)
                {
                    distance = tempAgent.remainingDistance;
                    tempWP = wP.transform;
                }
            }
        }
        return tempWP;
    }

    public void OnEnter() {
        Debug.Log("Enter SearchForWaypoint");
        meleeEnemy.Target = null;
        path = new NavMeshPath();
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
    }
    public void OnExit() {
        Debug.Log("Exit SearchForWaypoint");
        navMeshAgent.isStopped = false;
    }


}
