using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public GameObject spearHitPoint;
    public float sphereRadius;
    public int entityType;
    private Color sphereColor;

    private bool isHitting;
    private Coroutine hitCoroutine;

    public LayerMask HitBoxLayer;

    private StaminaManager SM;
    private Player player;

    private string currentTag;
    public Transform startRayPosition;
    public Transform endRayPosition;

    public float spearRange;

    // Start is called before the first frame update
    void Start()
    {
        SM = GetComponentInParent<StaminaManager>();
        player = GetComponentInParent<Player>();
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
        Debug.DrawRay(startRayPosition.position, (endRayPosition.position - startRayPosition.position).normalized * spearRange, Color.yellow, 2);
        if (Physics.Raycast(startRayPosition.position, (endRayPosition.position-startRayPosition.position), out hit, spearRange, HitBoxLayer))
        {
            Debug.Log(hit.collider.gameObject.name);
            currentTag = hit.collider.tag;
            hit.collider.GetComponentInParent<Enemy>().Hit(currentTag, attackID);
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
