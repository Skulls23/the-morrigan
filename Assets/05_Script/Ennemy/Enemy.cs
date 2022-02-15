using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AK.Wwise;

public class Enemy : Actor
{
    protected static int numberOfEnnemies = 0;

    [SerializeField]
    protected int id;
    protected bool isPlayerInZone = false;
    protected bool isPlayerSpotted = false;
    [SerializeField] protected float minDistFromTarget;

    public float lifeMax;
    public float currentLife;

    public float hitWeakpointDamages;
    public float hitFleshDamages;

    private int lastAttackId;

    public GameObject LockPoint;

    public Animator anim;

    protected virtual void Start()
    {
        if (anim)
        {
            anim.SetFloat("vertical", 1);
        }
        id = numberOfEnnemies;
        numberOfEnnemies++;
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

    public void Hit(string HitBoxTypeString, int attackId)
    {
        Debug.Log("Hit");
        Debug.Log(HitBoxTypeString + " " + HitBoxType.Flesh.ToString());
        if (lastAttackId == attackId)
            return;

        lastAttackId = attackId;
        float tempDamage = 0;
        if(HitBoxTypeString == HitBoxType.Flesh.ToString())
        {
            tempDamage = hitFleshDamages;
            AkSoundEngine.PostEvent("WEA_Hit_Flesh", gameObject);
        }
        else if (HitBoxTypeString == HitBoxType.WeakPoint.ToString())
        {
            tempDamage = hitWeakpointDamages;
            AkSoundEngine.PostEvent("WEA_Hit_WeakPoint", gameObject);
        }

        if (currentLife > tempDamage)
        {
            currentLife -= tempDamage;
        }
        else
        {
            currentLife = 0;
        }
        
    }

}

enum HitBoxType
{
    Flesh,
    WeakPoint,
}
