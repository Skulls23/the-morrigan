using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Header("GAME DESIGN")]
    [SerializeField]
    private float currentStamina;

    private float currentStaminaIndicator;

    public Image staminaBar;
    public Image staminaIndicator;

    Coroutine updateStaminaIndicator;
    private bool followGreenBar = true;

    Coroutine staminaRegen;
    bool sRRunning;

    public bool canRun;

    private Player player;
    private CharacterMovement CM;

    // Start is called before the first frame update
    void Start()
    {
        currentStaminaIndicator = currentStamina;
        player = GetComponent<Player>();
        CM = GetComponent<CharacterMovement>();
        followGreenBar = true;

        //Setup Base values
        player.AttackStaminaCost = player.BaseAttackStaminaCost;
        player.DashStaminaCost = player.BaseDodgeStaminaCost;
    }

    // Update is called once per frame
    void Update()
    {
        if (CM.GetIsRunning())
        {
            if (sRRunning)
            {
                StopRegen();
            }

            currentStamina -= Time.deltaTime * player.RunStaminaPerSecond;

            if(currentStamina <= 0)
            {
                canRun = false;
                currentStamina = 0;
            }
        }
        else
        {
            if ((!sRRunning))
            {
                StartRegen();
            }
        }

        if (followGreenBar)
        {
            currentStaminaIndicator = currentStamina;
        }

        staminaBar.fillAmount = currentStamina / 100;
        staminaIndicator.fillAmount = currentStaminaIndicator / 100;
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
            StartUpdateStaminaIndicator();
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
        sRRunning = true;
        staminaRegen = StartCoroutine("PassiveStaminaRegen");
    }

    void StopRegen()
    {
        StopCoroutine(staminaRegen);
        sRRunning = false;
    }

    IEnumerator PassiveStaminaRegen()
    {
        yield return new WaitForSeconds(player.TimeBeforeStartingRegenAgain);
        while (currentStamina < player.StaminaMax)
        {
            currentStamina += Time.deltaTime * player.StaminaPerSecond;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currentStamina = player.StaminaMax;
        sRRunning = false;
        yield return null;
    }

    IEnumerator UpdateStaminaIndicator()
    {
        yield return new WaitForSeconds(player.TimeBeforeUpdatingStaminaIndicator);
        while(currentStaminaIndicator > currentStamina)
        {
            currentStaminaIndicator -= Time.deltaTime * player.StaminaPerSecond;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currentStaminaIndicator = currentStamina;
        followGreenBar = true;
        yield return null;
    }

    void StartUpdateStaminaIndicator()
    {
        followGreenBar = false;
        updateStaminaIndicator = StartCoroutine("UpdateStaminaIndicator");
    }

    void StopUpdateStaminaIndicator()
    {
        StopCoroutine(updateStaminaIndicator);
    }
}


/*
 * Quand une action gaspille du stamina (roulade, attaques)
 * On appelle la coroutine
 * Dans x secondes on descend la barre rouge
 * si la barre verte rattrape la barre rouge
 * on stoppe la coroutine 
 * la valeur de la barre rouge = valeur du stamina de base
 * 
 * 
 */