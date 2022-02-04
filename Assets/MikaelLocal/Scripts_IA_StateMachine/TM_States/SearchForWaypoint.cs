using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SearchForWaypoint : IState
{
    private readonly MeleeEnemy meleeEnemy;

    public SearchForWaypoint(MeleeEnemy _meleeEnemy)
    {
        meleeEnemy = _meleeEnemy;
    }
    public void Tick()
    {
        meleeEnemy.Target = ChooseNearestWaypoint();
    }

    private Transform ChooseNearestWaypoint()
    {
        NavMeshAgent tempAgent = new NavMeshAgent();
        Transform tempWP = meleeEnemy.Waypoints[0].transform;
        NavMeshPath tempPath = null;
        float distance = -1;


        foreach (Waypoint wP in meleeEnemy.Waypoints)
        {
            tempAgent.destination = wP.transform.position;
            tempAgent.CalculatePath(wP.transform.position, tempPath);
            if(tempPath.status == NavMeshPathStatus.PathComplete)
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

    public void OnEnter() { }
    public void OnExit() { }


}
