using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthExample : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Corruption
        if (Input.GetKeyUp(KeyCode.I))
        {
            player.GetComponent<Health>().addCorruptedHealth();
        }

        //Damage
        if (Input.GetKeyUp(KeyCode.O))
        {
            player.GetComponent<Health>().takeDamage();
        }

        //Heal
        if (Input.GetKeyUp(KeyCode.P))
        {
            player.GetComponent<Health>().heal(3, 1f);
        }
    }
}
