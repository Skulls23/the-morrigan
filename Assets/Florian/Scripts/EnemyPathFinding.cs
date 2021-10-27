using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    public float detectionRadius = 15f;

    [SerializeField]
    private float StoppingRange = 5f;

    private NavMeshAgent agent;
     
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private GameObject player;

    private bool isPlayerInArea = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //startingPosition = transform.position;
        player = GameObject.Find("Player");
        roamPosition = VerifyNewPathIsPossible();
    }

    private void Update()
    {
        if (isPlayerInArea)
        {
            //reached destination
            if (Vector3.Distance(transform.position, player.transform.position) > StoppingRange)
                agent.SetDestination(player.transform.position);
            else
                agent.ResetPath();
                
        }
        else
        {
            agent.SetDestination(roamPosition);

            if (Vector3.Distance(transform.position, roamPosition) <= StoppingRange)
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
        Vector3 destination = RandomRoamingDestination(detectionRadius);

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);
        while (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            destination = RandomRoamingDestination(detectionRadius);
            agent.CalculatePath(destination, path);
        }

        return destination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }
}
