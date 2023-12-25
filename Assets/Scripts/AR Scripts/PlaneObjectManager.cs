using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneObjectManager : MonoBehaviour
{
    public GameObject spawn_prefab;
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
            if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                if (!object_spawned)
                {
                    spawned_object = Instantiate(spawn_prefab, hitpose.position, hitpose.rotation);
                    object_spawned = true;

                }
                else
                {
                    spawned_object.transform.position = hitpose.position;
                }
            }
        }
    }

}
