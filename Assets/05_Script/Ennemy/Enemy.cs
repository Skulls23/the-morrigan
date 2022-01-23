using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    [SerializeField]
    private int id;
    private bool isPlayerInZone = false;
    private bool isPlayerSpotted = false;
    [SerializeField] protected float minDistFromTarget;

    public GameObject LockPoint;

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

    public int GetId()
    {
        return id;
    }


}
