using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointsGuard : MonoBehaviour
{
    public List<GameObject> lCheckpoints;

    private NavMeshAgent agent;
    private GameObject player;

    private bool isPlayerInArea = false;
    private int  i = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }
}
