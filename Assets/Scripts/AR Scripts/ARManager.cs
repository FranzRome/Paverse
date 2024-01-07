using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class PlaneObjectManager : MonoBehaviour
{
    [SerializeField] GameObject spawn_libro;
    [SerializeField] GameObject spawn_pipa;
    [SerializeField] GameObject spawn_penna;
    [SerializeField] GameObject spawn_altro;

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


    // Update is called once per frame
    public void SpawnObject()
    {
        if (Input.touchCount > 0)
        {
            if(spawn_prefab == "spawn_penna")
            {
                SpawnObjectHelper(spawn_penna);
            }
            else if(spawn_prefab == "spawn_pipa")
            {
                SpawnObjectHelper(spawn_pipa);
            }
            else if (spawn_prefab == "spawn_libro")
            {
                SpawnObjectHelper(spawn_libro);
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
                spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
                object_spawned = true;

            }
            else
            {
                Destroy(spawned_object);
                spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GiraSinistra()
    {
        spawned_object.transform.Rotate(new Vector3(0, 35, 0));
    }
    public void GiraDestra()
    {
        spawned_object.transform.Rotate(new Vector3(0, -35, 0));
    }


    public void FineTurorial(GameObject tutorial)
    {
        tutorial.SetActive(false);
    }

}
