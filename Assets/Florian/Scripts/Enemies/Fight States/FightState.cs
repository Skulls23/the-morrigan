using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightState : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.GetChild(1).transform.position;
        if (GetComponent<Enemy>().IsPlayerInArea)
        {
            //reached destination
            if (Vector3.Distance(transform.position, playerPosition) > GetComponent<Enemy>().MinDistFromTarget)
                agent.SetDestination(playerPosition);
            else
                agent.ResetPath();
        }
    }
}
