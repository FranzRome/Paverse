using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TourManager : MonoBehaviour
{
    // UI
    public GameObject ui;
    public GameObject tutorialMobile;
    public GameObject tutorialWeb;
    public GameObject backButton;
    public GameObject joystick;

    /*
    // Audio
    public AudioSource audioSource;
    public AudioClip preTeleport;
    public AudioClip teleportStart;
    public AudioClip teleportLoop;
    public AudioClip teleportEnd;

    // Other Objects
    public GameObject player;
    */

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(ui);
        SceneManager.sceneLoaded += OnSceneLoaded; // Scene loaded event
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("Tutorial Played") != 1)
        {
#if !(UNITY_ANDROID || UNITY_IOS)
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
            PlayerPrefs.SetInt("Tutorial Played", 1);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        // Press Ctrl+R to reset tutorial flag
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("Tutorial Played", 0);
            print("Tutorial Reset");
        }
    }
#endif

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "3D Church")
        {
            TourManager[] tms = GameObject.FindObjectsByType<TourManager>(FindObjectsSortMode.InstanceID);
            foreach (TourManager tm in tms)
            {
                print(tm.name);
                if (!tm.Equals(this))
                {
                    Destroy(tm.gameObject);
                }
            }

            GameObject[] uis = GameObject.FindGameObjectsWithTag("UI");

            foreach (GameObject go in uis) {
                if (!go.Equals(this.ui)) {
                    Destroy(go);
                }
            }
        }
    }

    /*
    // Teleport audio management methods
    public void TpPre()
    {
        audioSource.clip = preTeleport;
        audioSource.Play();
    }

    public void TpStart()
    {
        audioSource.clip = teleportStart;
        audioSource.Play();
    }

    public void TpLoop()
    {
        audioSource.clip = teleportLoop;
        audioSource.Play();
    }

    public void TpEnd()
    {
        audioSource.clip = teleportEnd;
        audioSource.Play();
    }
    */
}
