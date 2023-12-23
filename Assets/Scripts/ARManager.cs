using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    float _rotationSpeed = 200f;

    public void ChangeScene(string sceneName)
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene(), UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadScene(sceneName);
    }

    public void GiraDestra()
    {
        GameObject.Find("ListaStanze").transform.Rotate(0, -_rotationSpeed * Time.deltaTime, 0);
        
    }

    public void GiraSinistra()
    {
        GameObject.Find("ListaStanze").transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);

    }
}
