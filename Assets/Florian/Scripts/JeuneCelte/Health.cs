using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The is the life system of the Jeune Celte
/// It has to be inside the Jeune Celte GameObject
/// It will be called by the Health Manager
/// </summary>
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
