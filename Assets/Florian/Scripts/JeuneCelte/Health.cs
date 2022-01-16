using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthMax;
    [SerializeField] private int healingValue;

    private int health;
    private int corruptedHealth;
    
    private int healToDo;

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

    /// <summary>
    /// Heal x hearths container
    /// Corrupted hearth container are healed first
    /// </summary>
    /// <param name="add">The number of health container healed, if 0, the var healingValue is took</param>
    /// <param name="timeBetweenHeal">The time between two heart to be healed</param>
    public void Heal(int add, float timeBetweenHeal)
    {
        if (add == 0)
            healToDo = healingValue;
        else
            healToDo = add;

        InvokeRepeating("InvokeHeal", 0f, timeBetweenHeal);
    }

    /// <summary>
    /// Stop the healing process.
    /// </summary>
    public void StopHeal()
    {
        CancelInvoke();
    }

    public int GetCorruptedHealth()
    {
        return corruptedHealth;
    }

    /// <summary>
    /// Convert a health container into a corrupted health container.
    /// The last health container can't be corrupted
    /// </summary>
    public void AddCorruptedHealth()
    {
        if (health > 1)
        {
            corruptedHealth++;
            health--;
        }
    }

    /// <summary>
    /// Destroy 1 health container and 1 corrupted health container if he exists
    /// </summary>
    public void TakeDamage()
    {
        if (health != 0 && corruptedHealth != 0)
        {
            health--;
            corruptedHealth--;
        }
        else if (health != 0)
            health--;

        if (health == 0)
            Debug.Log("Dead");
    }

    /// <summary>
    /// Launch the method AddHealth
    /// </summary>
    private void InvokeHeal()
    {
        if (healToDo == 0)
            CancelInvoke();
        else
            AddHealth();

        healToDo--;
    }

    /// <summary>
    /// Convert corrupted health container into health
    /// Recover health if no corrupted health container exists
    /// </summary>
    private void AddHealth()
    {

        if (corruptedHealth >= 1)
            corruptedHealth -= 1;
        else
            health += 1;

        if (health >= healthMax)
            health = healthMax;
    }
}
