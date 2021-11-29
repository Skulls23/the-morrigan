using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    private bool isPlayerInArea = false;
    [SerializeField] protected float minDistFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

    public float MinDistFromTarget
    {
        get {return this.minDistFromTarget;}
        set {this.minDistFromTarget = value;}
    }
    public bool IsPlayerInArea 
    {
        get { return this.isPlayerInArea; }
        set { this.isPlayerInArea = value; }
    }
}
