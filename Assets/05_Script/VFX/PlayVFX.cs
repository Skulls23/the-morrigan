using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using AK.Wwise;

public class PlayVFX : MonoBehaviour
{
    public ParticleSystem Vfx;
    public ParticleSystem Vfx2;
    public ParticleSystem Vfx3;

    public VisualEffect VisualEffect1;
    public VisualEffect VisualEffect2;
    public VisualEffect VisualEffect3;


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


    //-----------------VISUAL EFFECT-----------------//

    public void PlayVisualEffect()
    {
        VisualEffect1.Play();
    }

    public void PlayVisualEffect2()
    {
        VisualEffect2.Play();
    }
    public void PlayVisualEffect3()
    {
        VisualEffect3.Play();
    }

    public void StopVisualEffect()
    {
        VisualEffect1.Stop();
    }

    public void StopVisualEffect2()
    {
        VisualEffect2.Stop();
    }
    public void StopVisualEffect3()
    {
        VisualEffect3.Stop();
    }

    //SOUND

    public void PlaySound()
    {
        AkSoundEngine.PostEvent("ENV_Portal_Ouverture_Play", gameObject);
    }
}
