using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ApplyEffect(string name)
    {
        switch (name){
            case "Faveur d'Aeval" :
                Alpha = true;
                player.HealValueBase = player.HealValueAlpha;
                break;
            case "Bénédiction de Dagda":
                Bravo = true;
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
                PSM.ChangeMaxLives(player.MaxLives);
                break;

            case "Méfait de Miach":
                Echo = true;
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
                player.HealValueBase = player.HealValueAlpha;
                break;
            case "Bénédiction de Dagda":
                Bravo = true;
                if (Echo)
                {
                    player.DamageOnFleshBase = player.DamageOnFleshBravoAndEcho;
                    player.DamageOnWeakpointBase = player.DamageOnWeakpointBravoAndEcho;
                }
                else
                {
                    player.DamageOnFleshBase = player.DamageOnFleshBravo;
                    player.DamageOnWeakpointBase = player.DamageOnWeakpointBravo;
                }
                break;

            case "Résolution de Nuada":
                Charlie = true;
                player.AttackStaminaCost = player.AttackStaminaCostCharlie;
                player.DashStaminaCost = player.DodgeStaminaCostCharlie;
                break;

            case "Rétribution de Clíodhna":
                Delta = true;
                PSM.ChangeMaxLives(player.MaxLives);
                break;

            case "Méfait de Miach":
                Echo = true;
                if (Bravo)
                {
                    player.DamageOnFleshBase = player.DamageOnFleshBravoAndEcho;
                    player.DamageOnWeakpointBase = player.DamageOnWeakpointBravoAndEcho;
                }
                else
                {
                    player.DamageOnFleshBase = player.DamageOnFleshEcho;
                    player.DamageOnWeakpointBase = player.DamageOnWeakpointEcho;
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
}
