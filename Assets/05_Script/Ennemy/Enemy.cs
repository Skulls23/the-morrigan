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

    private int lastAttackId;

    //COMPONENTS
    public GameObject LockPoint;
    public Animator anim;
    private MeleeEnemy ME;

    public bool hasBeenHit;
    public bool IsDead;

    public bool isLocked;

    protected virtual void Start()
    {
        ME = GetComponent<MeleeEnemy>();
        if (anim)
        {
            anim.SetFloat("vertical", 1);
        }
        id = numberOfEnnemies;
        numberOfEnnemies++;
    }

    protected virtual void Update()
    {
        if (isLocked)
        {
            LockPoint.transform.LookAt(ME.Target.transform.position);
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

    public void Hit(string HitBoxTypeString, int attackId, float damages)
    {
        StartCoroutine(IEHit(HitBoxTypeString, attackId, damages));
    }

    private IEnumerator IEHit(string HitBoxTypeString, int attackId, float damages)
    {
        Debug.Log("Hit");
        Debug.Log(HitBoxTypeString + " " + HitBoxType.Flesh.ToString());
        hasBeenHit = true;
        yield return new WaitForEndOfFrame();
        hasBeenHit = false;

        if (lastAttackId == attackId)
            yield return null;

        lastAttackId = attackId;

        if (HitBoxTypeString == HitBoxType.Flesh.ToString())
        {
            AkSoundEngine.PostEvent("WEA_Hit_Flesh", gameObject);
        }
        else if (HitBoxTypeString == HitBoxType.WeakPoint.ToString())
        {
            AkSoundEngine.PostEvent("WEA_Hit_WeakPoint", gameObject);
        }

        if (currentLife > damages)
        {
            currentLife -= damages;
        }
        else if(!IsDead)
        {
            GameObject.Find("SceneManager").GetComponent<ScenesManager>()._nbEnnemies--;
            IsDead = true;
            currentLife = 0;
        }
    }

}

enum HitBoxType
{
    Flesh,
    WeakPoint,
}
