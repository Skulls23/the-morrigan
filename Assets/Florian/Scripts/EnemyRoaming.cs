using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoaming : MonoBehaviour
{
    [SerializeField] 
    private const float MAX_RANGE_ROAMING = 15f;

    private NavMeshAgent agent;
     
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private float reachedPositionDistance = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position;
        roamPosition = RandomNavmeshLocation(10f);
    }

    private void Update()
    {
        Debug.Log(this.name + " " + roamPosition);
        agent.SetDestination(roamPosition);
        
        //reached destination
        if (Vector3.Distance(transform.position, roamPosition) <= reachedPositionDistance)
        {
            roamPosition = RandomNavmeshLocation(MAX_RANGE_ROAMING);
        }
    }

    //Select a random position to go (roaming)
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius + transform.position;

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
