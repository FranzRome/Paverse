using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class PlaneObjectManager : MonoBehaviour
{
    public GameObject spawn_sedia;
    public GameObject spawn_porta;

    string spawn_prefab;

    GameObject xr_origin;

    GameObject spawned_object;
    bool object_spawned;
    ARRaycastManager arrayman;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        xr_origin = GameObject.Find("XR Origin");
        Debug.Log(xr_origin);
        object_spawned = false;
        arrayman = xr_origin.GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ChangeScene("Main Menu");
        }
    }

    // Update is called once per frame
    public void SpawnObject()
    {
        if (Input.touchCount > 0)
        {
            if(spawn_prefab == "spawn_sedia")
            {
                SpawnObjectHelper(spawn_sedia);
            }
            else if(spawn_prefab == "spawn_porta")
            {
                SpawnObjectHelper(spawn_porta);
            }

            
        }
    }

    public void ChangeSpawnPrefab(string prefab)
    {
        spawn_prefab = prefab;
    }


    public void SpawnObjectHelper(GameObject prefab)
    {
        if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;
            if (!object_spawned)
            {
                spawned_object = Instantiate(prefab, hitpose.position, hitpose.rotation);
                object_spawned = true;

            }
            else
            {
                Destroy(spawned_object);
                spawned_object = Instantiate(prefab, hitpose.position, hitpose.rotation);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
