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

    public int getHealthMax()
    {
        return healthMax;
    }

    public void setHealthMax(int num)
    {
        healthMax = num;
    }

    public int getHealth()
    {
        return health;
    }
    public void addHealth(int add)
    {
        health += add;
        if(health > healthMax)
            health = healthMax;
    }

    public int getCorruptedHealth()
    {
        return health;
    }

    public void addCorruptedHealth()
    {
        if (health > 1)
        {
            corruptedHealth++;
            health--;
        }
    }

    public void takeDamage()
    {
        if (health != 0 && corruptedHealth != 0)
        {
            health--;
            corruptedHealth--;
        }
        else if(health != 0)
            health--;

        if (health == 0)
            Debug.Log("Dead");
    }
}
