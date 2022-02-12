using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Header("GAME DESIGN")]
    [SerializeField]
    private float currentStamina;

    public Image staminaBar;

    Coroutine staminaRegen;
    bool sRRunning;

    public bool canRun;

    private Player player;
    private CharacterMovement CM;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        CM = GetComponent<CharacterMovement>();
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
}
