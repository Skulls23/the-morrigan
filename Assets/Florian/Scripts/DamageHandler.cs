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
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.LifePoints -= damage;
            Debug.Log(other.gameObject.name + " entered " + enemy.LifePoints);
            if (enemy.LifePoints <= 0)
                other.gameObject.SetActive(false);
        }
    }
}
