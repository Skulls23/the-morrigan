using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public int damage;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        //do damage to the enemy and deactivate him if he is dead
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.LifePoints -= damage;
            enemy.OnHit();
            if (enemy.LifePoints <= 0)
            {
                enemy.OnDeath();
                other.gameObject.SetActive(false);
            }
        }
    }
}
