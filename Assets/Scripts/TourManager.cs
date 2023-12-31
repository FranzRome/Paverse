using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TourManager : MonoBehaviour
{
    public GameObject tutorialMobile;
    public GameObject tutorialWeb;


    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        tutorialMobile.SetActive(false);
        tutorialWeb.SetActive(true);
#else
        tutorialMobile.SetActive(true);
        tutorialWeb.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
