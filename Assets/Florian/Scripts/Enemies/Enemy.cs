using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    private bool isPlayerInZone = false;
    private bool isPlayerSpotted = false;
    [SerializeField] protected float minDistFromTarget;

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
