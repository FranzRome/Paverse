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
    /*public void SpawnObject()
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
    }*/

    public void ChangeSpawnPrefab(GameObject prefab)
    {
        // spawn_prefab = prefab;
        int x = (Screen.width / 2);
        int y = (Screen.height / 2);
        if (arrayman.Raycast(new Vector2(x, y), hits, TrackableType.PlaneWithinPolygon))
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

    /*
    public void SpawnObjectHelper(GameObject prefab)
    {
        int x = (Screen.width/2);
        int y = (Screen.height/2);
        if (arrayman.Raycast(new Vector2(x,y), hits, TrackableType.PlaneWithinPolygon))
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
    }*/

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
