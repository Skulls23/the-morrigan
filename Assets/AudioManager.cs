using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
        AkSoundEngine.PostEvent("ENV_Menu_Music_Play", gameObject);
    }

   public void LaunchForest()
    {
        AkSoundEngine.PostEvent("ENV_Menu_Music_Stop", gameObject);
        AkSoundEngine.PostEvent("ENV_Amb_Forest", gameObject);
    }
}
