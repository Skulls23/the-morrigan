using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointsGuard : MonoBehaviour
{
    public List<GameObject> lCheckpoints;
    public List<float> lWaitEachCP;
    
    private NavMeshAgent agent;
    private int i = 0;
    private bool isWaiting = false;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(lCheckpoints[i].transform.position);
        GetComponent<EnemyAnimation>().SetDirection(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Enemy>().IsPlayerInZone || !GetComponent<Enemy>().IsPlayerSpotted)
        {
            if (agent.remainingDistance < 1f && !isWaiting)
            {
                isWaiting = true;
                GetComponent<EnemyAnimation>().SetDirection(0);
                StartCoroutine(Wait(lWaitEachCP[i++]));
            }
            if (i >= lCheckpoints.Count)
                i = 0;
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        agent.SetDestination(lCheckpoints[i].transform.position);
        GetComponent<EnemyAnimation>().SetDirection(1);
        isWaiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Enemy>().IsPlayerSpotted = true;
        }
    }

    /// <summary>
    /// Will set the player detected if he is in zone and in the enemy area
    /// </summary>
    /// <param name="state">Player is in the zone</param>
    public void ZoneColliderAlert(bool state)
    {
        if(state)
            GetComponent<Enemy>().IsPlayerInZone = true;
        else
        {
            //if the player left the zone but was followed by the AI.
            if (GetComponent<Enemy>().IsPlayerSpotted == true)
            {
                GetComponent<Enemy>().IsPlayerInZone = false;
                GetComponent<Enemy>().IsPlayerSpotted = false;
                i--;
            }
            else
            {
                GetComponent<Enemy>().IsPlayerInZone = false;
                GetComponent<Enemy>().IsPlayerSpotted = false;
            }
        }
    }
}
