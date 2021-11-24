using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointsGuard : MonoBehaviour
{
    public List<GameObject> lCheckpoints;
    private NavMeshAgent agent;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(lCheckpoints[i++].transform.position); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Enemy>().IsPlayerInArea)
        {
            if (agent.remainingDistance == 0)
                agent.SetDestination(lCheckpoints[i++].transform.position);
            if (i >= lCheckpoints.Count)
                i = 0;
        }
    }
}
