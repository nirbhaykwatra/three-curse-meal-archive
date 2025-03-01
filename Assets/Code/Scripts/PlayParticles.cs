using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] childrenParticleSytems;

    public void Playparticles()
    {
        foreach (ParticleSystem childPS in childrenParticleSytems)
        {
            childPS.Play();
        }
    }
}
