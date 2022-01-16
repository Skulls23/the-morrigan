using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("ENV_Amb_Forest", gameObject);
    }
}
