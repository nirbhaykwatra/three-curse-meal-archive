using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticlesTimings : MonoBehaviour
{
    [SerializeField] private ParticleSystem _footstepLeft;
    [SerializeField] private ParticleSystem _footstepRight;
    private int foot = 0;
    public void PlayPlayerFootsteps()
    {
        if (foot == 0)
        {
            _footstepLeft.Play();
            foot++;
        }
        else if(foot == 1)
        {
            _footstepRight.Play();
            foot--;
        }
    }
}
