using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject player;
    protected bool isPlayerInArea = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
