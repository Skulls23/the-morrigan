using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointsGuard : MonoBehaviour
{
    public List<GameObject> lCheckpoints;
    public List<float> lWaitEachCP;
    public float speed;

    private NavMeshAgent agent;
    private int i = 0;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(lCheckpoints[i].transform.position); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Enemy>().IsPlayerInArea)
        {
            if (agent.remainingDistance == 0 && !isWaiting)
            {
                StartCoroutine(Wait(lWaitEachCP[i++]));
                isWaiting = true;
            }
            if (i >= lCheckpoints.Count)
                i = 0;
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        agent.SetDestination(lCheckpoints[i].transform.position);
        isWaiting = false;
    }
}
