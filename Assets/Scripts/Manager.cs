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

        DontDestroyOnLoad(this.gameObject);
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
        if (Input.GetKey(KeyCode.Escape))
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
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            Destroy(GameObject.Find("Player"));
        }

        SceneManager.LoadScene(sceneName);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://fondazionecesarepavese.it/cesare-pavese-vita-opere/");
    }
}
