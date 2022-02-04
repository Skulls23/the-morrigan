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
        Debug.Log(tempTimeWaited);
        if(tempTimeWaited >= timeToWait)
        {
            meleeEnemy.Target = meleeEnemy.GetNextDestination();
        }
        TimeWaited = tempTimeWaited;
        tempTimeWaited += Time.deltaTime;
    }

    public void OnEnter() {
        
    }
    public void OnExit() {
        Debug.Log("Exited");
        tempTimeWaited = 0;
        TimeWaited = tempTimeWaited;
    }

    
}