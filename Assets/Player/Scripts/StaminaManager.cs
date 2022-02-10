using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Header("GAME DESIGN")]
    [Range(0, 100)]
    public float StaminaMax;
    [Range(0, 100)]
    public float currentStamina;

    public float runStaminaPerSecond;

    public float timeBeforeStartingRegenAgain;
    public float staminaPerSecond;

    public Image staminaBar;

    Coroutine staminaRegen;
    bool sRRunning;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.fillAmount = currentStamina / 100;
    }

    public void UseStamina(float value)
    {
        if (currentStamina >= value)
        {
            if (staminaRegen != null)
            {
                StopRegen();
            }
            currentStamina -= value;
            StartRegen();
        }      
    }

    public bool HasEnoughStamina(float cost)
    {
        if (cost < currentStamina)
            return true;
        else
            return false;
    }

    void StartRegen()
    {
        staminaRegen = StartCoroutine("PassiveStaminaRegen");
    }

    void StopRegen()
    {
        StopCoroutine(staminaRegen);
        sRRunning = false;
    }

    IEnumerator PassiveStaminaRegen()
    {
        yield return new WaitForSeconds(timeBeforeStartingRegenAgain);
        sRRunning = true;
        while (currentStamina < StaminaMax)
        {
            currentStamina += Time.deltaTime * staminaPerSecond;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currentStamina = StaminaMax;
        sRRunning = false;
        yield return null;
    }
}
