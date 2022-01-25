using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int baseHealingValue;
    [SerializeField] private float timeToHeal;
    [SerializeField] private readonly HealthUI healthUIScript;
    private Health healthScript;

    private int healToDo;



    private void Start()
    {
        healthScript = GetComponent<Health>();
    }

    /// <summary>
    /// Heal x hearths container
    /// Corrupted hearth container are healed first
    /// </summary>
    /// <param name="add">The number of health container healed, if 0, the var baseHealingValue is took</param>
    /// <param name="timeToHeal">The time between two heart to be healed</param>
    public void Heal(int add)
    {
        if (add == 0)
            healToDo = baseHealingValue;
        else
            healToDo = add;

        Invoke("InvokeHeal", timeToHeal);
    }

    /// <summary>
    /// Convert a health container into a corrupted health container.
    /// The last health container can't be corrupted
    /// </summary>
    public void AddCorruptedHealth()
    {
        if (healthScript.GetHealth() > 1)
        {
            healthScript.SetCorruptedHealth(healthScript.GetCorruptedHealth() + 1);
            healthScript.SetHealth(healthScript.GetHealth() - 1);
            
            CallRefresh();
        }
    }

    /// <summary>
    /// Destroy 1 health container and 1 corrupted health container if he exists
    /// </summary>
    public void TakeDamage()
    {
        if (healthScript.GetHealth() != 0 && healthScript.GetCorruptedHealth() != 0)
        {
            healthScript.SetCorruptedHealth(healthScript.GetCorruptedHealth() - 1);
            healthScript.SetHealth(healthScript.GetHealth() - 1); 
        }
        else if (healthScript.GetHealth() != 0)
            healthScript.SetHealth(healthScript.GetHealth() - 1);

        CallRefresh();

        if (healthScript.GetHealth() == 0)
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
            AddHealth(healToDo);
    }

    /// <summary>
    /// Convert corrupted health container into health
    /// Recover health if no corrupted health container exists
    /// </summary>
    /// <param name="heal">The number of heart that need to be healed</param>
    private void AddHealth(int heal)
    {

        if (healthScript.GetCorruptedHealth() >= 1 && heal > healthScript.GetCorruptedHealth())
        {
            heal -= healthScript.GetCorruptedHealth();
            healthScript.SetCorruptedHealth(0);
        }
        else if (healthScript.GetCorruptedHealth() >= 1 && heal <= healthScript.GetCorruptedHealth())
        {
            healthScript.SetCorruptedHealth(healthScript.GetCorruptedHealth() - heal);
            heal = 0;
        }

        if (heal > 0)
            healthScript.SetHealth(healthScript.GetHealth() + heal);

        if (healthScript.GetHealth() >= healthScript.GetHealthMax())
            healthScript.SetHealth(healthScript.GetHealthMax());

        CallRefresh();
    }

    /// <summary>
    /// Call the RefreshUI method from healthUIScript
    /// </summary>
    private void CallRefresh()
    {
        healthUIScript.RefreshUI(healthScript.GetHealth(), healthScript.GetCorruptedHealth(),
                healthScript.GetHealthMax() - (healthScript.GetHealth() + healthScript.GetCorruptedHealth()));
    }
}
