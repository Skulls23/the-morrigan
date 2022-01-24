using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthMax;

    private int health;
    private int corruptedHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        corruptedHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health + " | " + corruptedHealth + " | " + healthMax);
    }

    public int GetHealthMax()
    {
        return healthMax;
    }

    public void SetHealthMax(int num)
    {
        healthMax = num;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int num)
    {
        health = num;

        if (health > healthMax)
            health = healthMax;

        if (health < 0)
            health = 0;
    }

    public int GetCorruptedHealth()
    {
        return corruptedHealth;
    }

    public void SetCorruptedHealth(int num)
    {
        corruptedHealth = num;
    }
}
