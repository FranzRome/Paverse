using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public GameObject list;
    public GameObject detail;

    void Start()
    {
        detail.SetActive(false);
        list.SetActive(true);
    }

    public void SelectElement(ListElement element)
    {
        list.SetActive(false);
        detail.SetActive(true);

        GameObject.Find("Detail Name").GetComponent<TMP_Text>().text = element.name;
        GameObject.Find("Detail Image").GetComponent<Image>().sprite = element.sprite;
        GameObject.Find("Detail Description").GetComponent<TMP_Text>().text = element.description;
    }

    public void GoToList()
    {
        detail.SetActive(false);
        list.SetActive(true);
    }
}
