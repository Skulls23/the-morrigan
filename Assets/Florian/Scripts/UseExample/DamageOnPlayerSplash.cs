using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnPlayerSplash : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float delayBetweenDamage;

    private bool DamageOverTime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamageOverTime = true;
            InvokeRepeating("TakeDamageOverTime", 0f, delayBetweenDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DamageOverTime = false;
    }

    private void TakeDamageOverTime()
    {
        if(DamageOverTime == true)
        {
            player.GetComponent<HealthManager>().TakeDamage();
        } 
    }
}
