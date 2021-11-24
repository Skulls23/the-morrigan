using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointsGuard : Enemy
{
    public List<GameObject> lCheckpoints;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(lCheckpoints[i++].transform.position); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerInArea)
        {
            if (agent.remainingDistance == 0)
                agent.SetDestination(lCheckpoints[i++].transform.position);
            if (i >= lCheckpoints.Count)
                i = 0;
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
