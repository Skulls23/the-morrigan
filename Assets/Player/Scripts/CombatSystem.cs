using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public GameObject spearHitPoint;
    public float sphereRadius;
    public int entityType;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void Hit()
    {
        Collider[] colliders = Physics.OverlapSphere(spearHitPoint.transform.position, sphereRadius);
        if(colliders.Length == 0)
        {
            entityType = 0;
            Gizmos.color = Color.blue;
        }
        foreach(Collider col in colliders)
        {
            if(col.gameObject.tag == "Flesh")
            {
                entityType = 1;
                Gizmos.color = Color.red;
                break;
            }
            if (col.gameObject.tag == "WeakPoint")
            {
                entityType = 2;
                Gizmos.color = Color.magenta;
                break;
            }
        }
        Debug.LogAssertion(entityType);
    }

    private void ResetColor()
    {
        Gizmos.color = Color.green;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spearHitPoint.transform.position, sphereRadius);
    }
}
