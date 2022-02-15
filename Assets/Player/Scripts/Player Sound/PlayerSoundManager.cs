using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public Animator CharacterAnimator;
    private CombatSystem cs;

    private void Start()
    {
        CharacterAnimator = CharacterAnimator.GetComponent<Animator>();
        cs = gameObject.GetComponent<CombatSystem>();
    }

    void PlayHitVocals001()
    {
        AkSoundEngine.PostEvent("CHA_Hit_Vocals_001", gameObject);
    }

    void PlayHit()
    {
        switch (cs.entityType)
        {
            case 0:
                AkSoundEngine.PostEvent("WEA_Hit_Swoosh", gameObject);
                break;
            default:
                AkSoundEngine.PostEvent("WEA_Hit_Swoosh", gameObject);
                break;
        }


    }

}
