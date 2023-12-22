using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnGrab : MonoBehaviour
{
    public ParticleSystem smokeParticles;

    void Start()
    {

    }

    void StartSmoke()
    {
        smokeParticles.Play();
    }

    void StopSmoke()
    {
        smokeParticles.Stop();
    }
}
