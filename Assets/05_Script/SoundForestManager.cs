using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().LaunchForest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
