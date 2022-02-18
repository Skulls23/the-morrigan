using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    //COMPONENTS
    private StaminaManager SM;
    private Player player;
    public UI_Player_Stats_Manager UIPSManager;

    //COMBAT SYSTEM
    public GameObject spearHitPoint;
    public Transform startRayPosition;
    public Transform endRayPosition;
    public LayerMask HitBoxLayer;

    private float currentDamageOnFlesh;
    private float currentDamageOnWeakpoint;
    private bool isHitting;
    private Coroutine hitCoroutine;
    private string currentTag;

    //DEBUGGING
    public float sphereRadius;
    private Color sphereColor;

    
    
    // Start is called before the first frame update
    void Start()
    {
        SM = GetComponentInParent<StaminaManager>();
        player = GetComponentInParent<Player>();
        currentDamageOnFlesh = player.DamageOnFleshBase;
        currentDamageOnWeakpoint = player.DamageOnWeakpointBase;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void UseStamina(ActionsCostingStamina action)
    {
        if(action == ActionsCostingStamina.NormalAttack)
        {
            SM.UseStamina(player.AttackStaminaCost);
        }
        if (action == ActionsCostingStamina.Dash)
        {
            SM.UseStamina(player.DashStaminaCost);
        }

    }

    private void StartHit()
    {
        isHitting = true;
        int id = Random.Range(0, 10000);
        hitCoroutine = StartCoroutine(Hit(id));
    }

    private void StopHit()
    {
        isHitting = false;
        StopCoroutine(hitCoroutine);
    }

    IEnumerator Hit(int attackID)
    {
        RaycastHit hit;
        Debug.DrawRay(startRayPosition.position, (endRayPosition.position - startRayPosition.position).normalized * player.SpearRange, Color.yellow, 2);
        if (Physics.Raycast(startRayPosition.position, (endRayPosition.position-startRayPosition.position), out hit, player.SpearRange, HitBoxLayer))
        {
            Debug.Log(hit.collider.gameObject.name);
            currentTag = hit.collider.tag;
            float tempDamages = 0;
            if (currentTag == HitBoxType.Flesh.ToString())
            {
                UIPSManager.PlayerGetCorrupted();
                tempDamages = currentDamageOnFlesh;
            }
            else if (currentTag == HitBoxType.WeakPoint.ToString())
            {
                UIPSManager.PlayerHitsWeakpoint();
                tempDamages = currentDamageOnFlesh;
            }
            hit.collider.GetComponentInParent<Enemy>().Hit(currentTag, attackID,tempDamages);
        }
        else
        {
            AkSoundEngine.PostEvent("WEA_Hit_Swoosh", gameObject);
        }
        yield return null;
    }

    private void ResetColor()
    {
        sphereColor = Color.green;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(spearHitPoint.transform.position, sphereRadius);
    }
}

enum ActionsCostingStamina
{
    NormalAttack,
    Dash,
}
