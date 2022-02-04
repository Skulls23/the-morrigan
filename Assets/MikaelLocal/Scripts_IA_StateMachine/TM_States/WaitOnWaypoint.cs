using System.Linq;
using UnityEngine;

public class WaitOnWaypoint : IState
{
    private readonly MeleeEnemy meleeEnemy;
    private float tempTimeWaited = 0;
    public float TimeWaited;
    public float timeToWait;

    public WaitOnWaypoint(MeleeEnemy _meleeEnemy)
    {
        meleeEnemy = _meleeEnemy;
        timeToWait = meleeEnemy.Waypoints[meleeEnemy.currentIndex].timeToWait;
    }
    public void Tick()
    {
        if(tempTimeWaited >= timeToWait)
        {
            meleeEnemy.Target = meleeEnemy.GetNextDestination();
        }
        TimeWaited = tempTimeWaited;
        tempTimeWaited += Time.deltaTime;
    }

    public void OnEnter() {
        Debug.Log("Enter WaitOnWaypoint");
    }
    public void OnExit() {
        Debug.Log("Exit WaitOnWaypoint");
        tempTimeWaited = 0;
        TimeWaited = tempTimeWaited;
    }

    
}