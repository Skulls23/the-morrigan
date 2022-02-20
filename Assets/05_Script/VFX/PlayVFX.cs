using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class PlayVFX : MonoBehaviour
{
    public ParticleSystem Vfx;
    public ParticleSystem Vfx2;
    public ParticleSystem Vfx3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayVFXOneShot()
    {
        Vfx.Play();
    }

    public void PlayVFXOneShot2()
    {
        Vfx2.Play();
    }
    public void PlayVFXOneShot3()
    {
        Vfx3.Play();
    }
    public void StopVFXOneShot()
    {
        Vfx.Stop();
    }
    public void StopVFXOneShot2()
    {
        Vfx2.Stop();
    }
    public void StopVFXOneShot3()
    {
        Vfx3.Stop();
    }
}
