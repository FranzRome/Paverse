using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public ListElement[] elements;
    public Manager manager;
    public GameObject elementPrefab;
    public Transform content;


    // Start is called before the first frame update
    void Start()
    {
        foreach(ListElement el in elements)
        {
            GameObject elementGameObject = Instantiate(elementPrefab);
            elementGameObject.transform.SetParent(content, false);
            elementGameObject.transform.GetChild(0).GetComponent<Image>().sprite = el.sprite;
            elementGameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = el.name;
            elementGameObject.GetComponent<Button>().onClick.AddListener(() => { manager.SelectElement(el); });
        }
    }
}
