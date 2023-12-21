using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Changing Scene"+sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://fondazionecesarepavese.it/cesare-pavese-vita-opere/");
    }
}
