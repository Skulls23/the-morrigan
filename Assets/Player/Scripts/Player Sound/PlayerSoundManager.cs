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
        Debug.Log("bite");
    }

    void PlayHit()
    {
        switch (cs.entityType)
        {
            case 0:
                AkSoundEngine.PostEvent("WEA_Hit_Swoosh", gameObject);
                break;
            case 1:
                AkSoundEngine.PostEvent("WEA_Hit_Flesh", gameObject);
                break;
            case 2:
                AkSoundEngine.PostEvent("WEA_Hit_WeakPoint", gameObject);
                break;
            default:
                AkSoundEngine.PostEvent("WEA_Hit_Swoosh", gameObject);
                break;
        }


    }

}
