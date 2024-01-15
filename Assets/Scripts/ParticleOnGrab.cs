using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ParticleOnGrab : MonoBehaviour
{
    public ParticleSystem smokeParticles;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(StartSmoke);
        grabInteractable.onSelectExited.AddListener(StopSmoke);
    }

    void StartSmoke(XRBaseInteractor interactor)
    {
        smokeParticles.Play();
    }

    void StopSmoke(XRBaseInteractor interactor)
    {
        smokeParticles.Stop();
    }
}
