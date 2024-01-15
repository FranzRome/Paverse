using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DimLightsOnCollision : MonoBehaviour
{
    public Light[] spotlights;  // Assicurati di collegare le spotlight nell'ispettore Unity

    public float dimSpeed = 0.5f;  // Velocità a cui si abbassa l'intensità delle luci

    private bool triggered = false;

    

    void Update()
    {
        if (triggered)
        {
            DimSpotlights();
         
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("persona"))
        {
            triggered = true;
        }
    }

    void DimSpotlights()
    {
        foreach (Light spotlight in spotlights)
        {
            spotlight.intensity -= dimSpeed * Time.deltaTime;

            if (spotlight.intensity <= 0f)
            {
                spotlight.intensity = 0f;
                // Puoi aggiungere ulteriori azioni o chiamare altre funzioni quando le luci si spengono completamente
            }
        }
    }

    
}
