using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public GameObject spearHitPoint;
    public float sphereRadius;
    public int entityType;
    private Color sphereColor;

    public LayerMask HitBoxLayer;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void UseStamina()
    {

    }

    private void Hit()
    {
        Collider[] colliders = Physics.OverlapSphere(spearHitPoint.transform.position, sphereRadius, HitBoxLayer);
        if(colliders.Length == 0)
        {
            entityType = 0;
            sphereColor = Color.blue;
            Debug.Log(entityType);
        }
        else
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject.tag == "Flesh")
                {
                    entityType = 1;
                    sphereColor = Color.magenta;
                    Debug.Log(entityType);
                    return;
                }
                if (col.gameObject.tag == "WeakPoint")
                {
                    entityType = 2;
                    sphereColor = Color.red;
                    Debug.Log(entityType);
                    return;
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
