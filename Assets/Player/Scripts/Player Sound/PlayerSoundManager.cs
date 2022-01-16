using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public Animator CharacterAnimator;

    private void Start()
    {
        CharacterAnimator = CharacterAnimator.GetComponent<Animator>();
    }

    void PlayFoostepWalk()
    {
        AkSoundEngine.PostEvent("CHA_Footsteps_Walk", gameObject);
        Debug.Log("bite");
    }
}
