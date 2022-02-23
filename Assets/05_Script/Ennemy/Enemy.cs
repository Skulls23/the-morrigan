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

    private Material[] enemyMaterials;

    //COMPONENTS
    public GameObject LockPoint;
    public GameObject CameraHitbox;
    public Animator anim;
    private MeleeEnemy ME;

    public bool hasBeenHit;
    public bool IsDead;

    public bool isLocked;
    public bool isOnCameraFieldOfView;

    public Collider EnemyCollider;

    protected virtual void Start()
    {
        ME = GetComponent<MeleeEnemy>();
        if (anim)
        {
            anim.SetFloat("vertical", 1);
        }
        id = numberOfEnnemies;
        numberOfEnnemies++;

        enemyMaterials = GetComponentInChildren<MeshRenderer>().materials;
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

    public void Hit(string HitBoxTypeString, int attackId, float damages, CharacterMovement CM)
    {
        StartCoroutine(IEHit(HitBoxTypeString, attackId, damages, CM));
    }

    private IEnumerator IEHit(string HitBoxTypeString, int attackId, float damages, CharacterMovement CM)
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
            Die(CM);
        }
    }

    private void Die(CharacterMovement CM)
    {
        EnemyCollider.enabled = false;
        GetComponent<MeleeEnemy>().AttackCollider.gameObject.SetActive(false);
        CameraHitbox.SetActive(false);
        CM.UIPSManager.PlayerHitsWeakpoint();
        StartCoroutine("FadeEnemy");
        if (isOnCameraFieldOfView)
        {
            CM.GetComponent<CameraController>().DDC.RemoveEnemyFromPool(id);
            if (isLocked)
            {
                CM.GetComponent<CameraController>().DeLock();
            }
        }
        if (GameObject.Find("SceneManager"))
        {
            GameObject.Find("SceneManager").GetComponent<ScenesManager>()._nbEnnemies--;
        }
        IsDead = true;
        currentLife = 0;
    }

    private IEnumerator FadeEnemy()
    {
        float time = 0;
        while (time<1)
        {
            foreach(Material mat in enemyMaterials)
            {
                Color col = mat.color;
                col = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Time.deltaTime);
                mat.color = col;
            }
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}

enum HitBoxType
{
    Flesh,
    WeakPoint,
}
