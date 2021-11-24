using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightState : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy>().IsPlayerInArea)
        {
            Debug.Log(name + " player here");
            //reached destination
            if (Vector3.Distance(transform.position, player.transform.position) > GetComponent<Enemy>().MinDistFromTarget)
                agent.SetDestination(player.transform.position);
            else
                agent.ResetPath();
        }
    }
}
