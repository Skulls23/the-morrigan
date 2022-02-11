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
            /*//HitBoxLayer, QueryTriggerInteraction.Collide
            Collider[] colliders = Physics.OverlapSphere(spearHitPoint.transform.position, sphereRadius);
            Debug.Log("1 loop with : " + colliders.Length + " colliders in it");
            if (colliders.Length == 0)
            {
                entityType = 0;
                sphereColor = Color.blue;
                //Debug.Log(entityType);
            }
            else
            {
                foreach (Collider col in colliders)
                {
                    Debug.Log(col.gameObject.name);
                    if (col.gameObject.tag == "Flesh")
                    {
                        //col.GetComponentInParent<Enemy>().Hit("Flesh", attackID);
                        entityType = 1;
                        sphereColor = Color.magenta;
                        //Debug.Log(entityType);
                        continue;
                    }
                    else if (col.gameObject.tag == "WeakPoint")
                    {
                        //col.GetComponentInParent<Enemy>().Hit("WeakPoint", attackID);
                        entityType = 2;
                        sphereColor = Color.red;
                        //Debug.Log(entityType);
                        continue;
                    }
                }
            }*/
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
        Debug.Log("the attack id is : " + attackID);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Collider[] colliders = Physics.OverlapSphere(spearHitPoint.transform.position, sphereRadius, HitBoxLayer, QueryTriggerInteraction.Collide);
            Debug.Log("1 loop with : " + colliders.Length + " colliders in it");
            if (colliders.Length == 0)
            {
                entityType = 0;
                sphereColor = Color.blue;
                //Debug.Log(entityType);
            }
            else
            {
                foreach (Collider col in colliders)
                {
                    Debug.Log(col.gameObject.name);
                    if (col.gameObject.tag == "Flesh")
                    {
                        col.GetComponentInParent<Enemy>().Hit("Flesh", attackID);
                        entityType = 1;
                        sphereColor = Color.magenta;
                        Debug.Log(entityType);
                        //continue;
                    }
                    else if (col.gameObject.tag == "WeakPoint")
                    {
                        col.GetComponentInParent<Enemy>().Hit("WeakPoint", attackID);
                        entityType = 2;
                        sphereColor = Color.red;
                        Debug.Log(entityType);
                        //continue;
                    }
                }
            }
        }
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
