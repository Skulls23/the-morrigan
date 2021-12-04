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
