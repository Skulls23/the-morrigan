using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    [SerializeField]
    protected int id;
    protected bool isPlayerInZone = false;
    protected bool isPlayerSpotted = false;
    [SerializeField] protected float minDistFromTarget;

    public GameObject LockPoint;

    public Animator anim;

    protected virtual void Start()
    {
        if (anim)
        {
            anim.SetFloat("vertical", 1);
        }
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

    public int GetId()
    {
        return id;
    }


}
