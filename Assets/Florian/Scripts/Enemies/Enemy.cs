using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    private bool isPlayerInZone = false;
    private bool isPlayerSpotted = false;
    [SerializeField] protected float minDistFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float MinDistFromTarget
    {
        get {return this.minDistFromTarget;}
        set {this.minDistFromTarget = value;}
    }
    public bool IsPlayerInZone
    {
        get { return this.isPlayerInZone; }
        set { this.isPlayerInZone = value; }
    }

    public bool IsPlayerSpotted
    {
        get { return this.isPlayerSpotted; }
        set { this.isPlayerSpotted = value; }
    }
}
