using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{

    public string destination; // Destination Scene Name

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se il collider con cui stiamo collidendo ha il tag desiderato
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().BeginTeleport(destination);
        }
    }
}
