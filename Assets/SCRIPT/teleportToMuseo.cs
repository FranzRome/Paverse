using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportToMuseo : MonoBehaviour
{
    // Specifica il nome della scena di destinazione
    public string museo;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se il collider con cui stiamo collidendo ha il tag desiderato
        if (other.CompareTag("persona"))
        {
            // Teletrasportati alla scena di destinazione
            SceneManager.LoadScene(museo);
        }
    }
}
