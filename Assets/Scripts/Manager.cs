using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.Loading;

public class Manager : MonoBehaviour
{

    public GameObject loading_panel;
    public Slider progress_slider;


    void Start()
    {
        //ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        //Screen.fullScreen = false;

        if (SceneManager.GetActiveScene().name.Contains("3D"))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        } 
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "List Menu")
            {
                ChangeScene("Main Menu");
            }
            else if (SceneManager.GetActiveScene().name.Contains("3D"))
            {
                Application.Quit();
            }
 
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName.Contains("3D"))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;

            //ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
            //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;

            //Screen.fullScreen = true;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            /*
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("UI 3D Tour Variant"));
            Destroy(GameObject.Find("Tour Manager"));
            */
        }

        // Exiting from 3D tour
        if (SceneManager.GetActiveScene().name.Contains("3D") && !sceneName.Contains("3D"))
        {
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("Tour Manager"));
            Destroy(GameObject.Find("UI 3D Tour Variant"));

            //ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
            //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
            //Screen.fullScreen = false;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://fondazionecesarepavese.it/cesare-pavese-vita-opere/");
    }


    public void ChiediSeARCoreInstallato(GameObject ar_panel)
    {
        ar_panel.SetActive(true);
    }

    public void ARCoreNonInstallato(GameObject ar_panel)
    {
        ar_panel.SetActive(false);
    }

    public void OpenDownloadARCore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.google.ar.core");
    }

    public void Load3dScene(string scene_name)
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.fullScreen = true;
        StartCoroutine(Load3DScene_Coroutine(scene_name));


    }

    public IEnumerator Load3DScene_Coroutine(string scene_name)
    {
        progress_slider.value = 0;
        loading_panel.SetActive(true);


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene_name);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progress_slider.value = progress;
            if(progress >= 0.9f)
            {
                progress_slider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
