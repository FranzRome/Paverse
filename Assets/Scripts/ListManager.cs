using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public ListElement[] elements;
    public GameObject elementPrefab;
    public Transform content;
    public GameObject list;
    public GameObject detail;

    // Start is called before the first frame update
    void Start()
    {
        detail.SetActive(false);
        list.SetActive(true);

        foreach (ListElement el in elements)
        {
            GameObject elementGameObject = Instantiate(elementPrefab);
            elementGameObject.transform.SetParent(content, false);
            elementGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = el.name;
            elementGameObject.GetComponent<Button>().onClick.AddListener(() => { SelectElement(el); });
        }
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
