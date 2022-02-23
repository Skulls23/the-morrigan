using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerBlessingsHandler : MonoBehaviour
{
    public Player player;
    public CombatSystem CombatS;
    public UI_Player_Stats_Manager PSM;

    private bool Alpha;
    private bool Bravo;
    private bool Empty;
    private bool Charlie;
    private bool Delta;
    private bool Echo;
    private bool Foxtrot;
    private bool Golf;
    private bool Hotel;

    public ParticleSystem EchoVFX;
    public VisualEffect BravoVisualEffect;

    public void ActivateVfx(ParticleSystem vfx)
    {
        vfx.Play();
    }

    public void DesactivateVfx(ParticleSystem vfx)
    {
        vfx.Stop();
    }

    public void ActivateVfx(VisualEffect vfx)
    {
        vfx.Play();
    }

    public void DesactivateVfx(VisualEffect vfx)
    {
        vfx.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyEffect("Faveur d'Aeval");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ApplyEffect("Bénédiction de Dagda");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Nuada");
            ApplyEffect("Résolution de Nuada");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ApplyEffect("Rétribution de Clíodhna");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ApplyEffect("Méfait de Miach");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            RemoveEffect("Faveur d'Aeval");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            RemoveEffect("Bénédiction de Dagda");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            RemoveEffect("Résolution de Nuada");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            RemoveEffect("Rétribution de Clíodhna");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            RemoveEffect("Méfait de Miach");
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyEffect("name");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyEffect("name");
        }*/
    }


    public void ApplyEffect(string name)
    {
        switch (name){
            case "Faveur d'Aeval" :
                Alpha = true;
                player.HealValue = player.HealValueAlpha;
                break;
            case "Bénédiction de Dagda":
                Bravo = true;
                Debug.Log("activate");
                ActivateVfx(BravoVisualEffect);
                if (Echo)
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBravoAndEcho, player.DamageOnWeakpointBravoAndEcho);
                }
                else
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBravo, player.DamageOnWeakpointBravo);
                }
                
                break;

            case "Résolution de Nuada":
                Charlie = true;
                player.AttackStaminaCost = player.AttackStaminaCostCharlie;
                player.DashStaminaCost = player.DodgeStaminaCostCharlie;
                break;

            case "Rétribution de Clíodhna":
                Delta = true;
                Debug.Log("MaxLife");
                PSM.ChangeMaxLives(player.MaxLivesDelta);
                break;

            case "Méfait de Miach":
                Echo = true;
                ActivateVfx(EchoVFX);
                if (Bravo)
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBravoAndEcho, player.DamageOnWeakpointBravoAndEcho);
                }
                else
                {
                    CombatS.UpdateDamages(player.DamageOnFleshEcho, player.DamageOnWeakpointEcho);
                }
                break;

            case "Pardon de Cernunnos":
                Foxtrot = true;
                //Code Corruption resistance (1 per room)
                break;
            case "Bénédiction de Lug":
                Golf = true;
                //Code resistance while healing
                break;
            case "Présent de Mongfind":
                Hotel = true;
                //Code Score
                break;
            default:

                break;

        }
    }

    public void RemoveEffect(string name)
    {
        switch (name)
        {
            case "Faveur d'Aeval":
                Alpha = false;
                player.HealValue = player.HealValueBase;
                break;
            case "Bénédiction de Dagda":
                Bravo = false;
                DesactivateVfx(BravoVisualEffect);
                if (Echo)
                {
                    CombatS.UpdateDamages(player.DamageOnFleshEcho, player.DamageOnWeakpointEcho);
                }
                else
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBase, player.DamageOnWeakpointBase);
                }
                break;

            case "Résolution de Nuada":
                Charlie = false;
                player.AttackStaminaCost = player.BaseAttackStaminaCost;
                player.DashStaminaCost = player.BaseDodgeStaminaCost;
                break;

            case "Rétribution de Clíodhna":
                Delta = false;
                PSM.ChangeMaxLives(player.MaxLivesBase);
                break;

            case "Méfait de Miach":
                Echo = false;
                DesactivateVfx(EchoVFX);
                if (Bravo)
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBravo, player.DamageOnWeakpointBravo);
                }
                else
                {
                    CombatS.UpdateDamages(player.DamageOnFleshBase, player.DamageOnWeakpointBase);
                }
                break;

            case "Pardon de Cernunnos":
                Foxtrot = false;
                //Code Corruption resistance (1 per room)
                break;
            case "Bénédiction de Lug":
                Golf = false;
                //Code resistance while healing
                break;
            case "Présent de Mongfind":
                Hotel = false;
                //Code Score
                break;
            default:

                break;

        }
    }
}
