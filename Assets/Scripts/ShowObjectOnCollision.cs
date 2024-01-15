using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // L'oggetto da far apparire/disparire
    public GameObject objectToShow;
    public GameObject[] objectsToHide;

    private void OnTriggerEnter(Collider collision)
    {
        //print("collision");
        //print(collision.gameObject.tag);
        // Verifica se l'oggetto colliso ha un certo tag (puoi personalizzarlo)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Attiva o disattiva l'oggetto da far apparire/disparire
            objectToShow.SetActive(true);

            // Disattiva tutti gli oggetti da far sparire
            foreach(GameObject go in objectsToHide)
            {
                go.SetActive(false);
            }
        }
    }
}
