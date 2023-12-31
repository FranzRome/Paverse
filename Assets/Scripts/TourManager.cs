using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TourManager : MonoBehaviour
{
    public GameObject tutorialMobile;
    public GameObject tutorialWeb;
    public GameObject backButton;
    public GameObject joystick;


    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        tutorialMobile.SetActive(false);
        tutorialWeb.SetActive(true);
        backButton.SetActive(false);
        joystick.SetActive(false);
#else
        tutorialMobile.SetActive(true);
        tutorialWeb.SetActive(false);
        backButton.SetActive(true);
        joystick.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
