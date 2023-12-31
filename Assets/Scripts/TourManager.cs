using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TourManager : MonoBehaviour
{
    public GameObject tutorial;

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        tutorial.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
