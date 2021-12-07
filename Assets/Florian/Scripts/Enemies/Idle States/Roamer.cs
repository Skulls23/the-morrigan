using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roamer : MonoBehaviour
{
    public float roamingRadius = 15f;

    private NavMeshAgent agent;
    private Vector3 roamPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamPosition = VerifyNewPathIsPossible();
    }

    private void Update()
    {
        if (!GetComponent<Enemy>().IsPlayerInZone || !GetComponent<Enemy>().IsPlayerSpotted)
        {
            
            agent.SetDestination(roamPosition);

            if (Vector3.Distance(transform.position, roamPosition) <= GetComponent<Enemy>().MinDistFromTarget)
                roamPosition = VerifyNewPathIsPossible();
        }
    }

    //Select a random position to go (roaming)
    public Vector3 RandomRoamingDestination(float radius)
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

    //Verify if the path given from RandomRoamingDestination is possible and find another destination if not
    public Vector3 VerifyNewPathIsPossible()
    {
        Vector3 destination = RandomRoamingDestination(roamingRadius);

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);
        while (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            destination = RandomRoamingDestination(roamingRadius);
            agent.CalculatePath(destination, path);
        }

        return destination;
    }
}
