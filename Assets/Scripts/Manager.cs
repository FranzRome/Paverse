using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Manager : MonoBehaviour
{
    void Start()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;

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
            if (
                SceneManager.GetActiveScene().name == "List Menu" ||
                SceneManager.GetActiveScene().name == "AR Scene" ||
                SceneManager.GetActiveScene().name == "New AR Scene" ||
                SceneManager.GetActiveScene().name == "3D Tour"
                )
            {
                ChangeScene("Main Menu");
            }
            else if(SceneManager.GetActiveScene().name == "Museo Pavesiano")
            {
                ChangeScene("List Menu");
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName.Contains("3D"))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;

            ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
            ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            Destroy(GameObject.Find("Player"));
        }

        // Exiting from 3D tour
        if (SceneManager.GetActiveScene().name == "3D Church" && !sceneName.Contains("3D"))
        {
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("Tour Manager"));
            Destroy(GameObject.Find("UI 3D Tour"));
        }

        SceneManager.LoadScene(sceneName);
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
}
