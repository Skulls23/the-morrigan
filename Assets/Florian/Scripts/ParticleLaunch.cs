using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLaunch : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    // Start is called before the first frame update
    private void Start()
    {
        particle = GameObject.Find("FallParticles").GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
            particle.Play();
    }
}
