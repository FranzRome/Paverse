using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float xRotationLowerLimit = -45, xRotationUpperLimit = 60;

    private int prevTouchCount = 0;
    private Vector2 prevTouchPosition = Vector2.zero;

    private void Update()
    {
        int touchCount = Input.touchCount;
        Vector2 touchPosition = Vector2.zero;
        Vector2 touchDelta = Vector2.zero;

        if(touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;

            //Debug.Log("Touches > 0");
            Debug.Log("Touch delta: " + touchDelta);

            if(prevTouchCount == 0)
            {
                prevTouchPosition = touchPosition;
            }

            touchDelta = touchPosition - prevTouchPosition;

            //Rotate the player object on the y axis and the camera on x axis to maintain independent rotation and not mess up with z axis
            transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(0f, touchDelta.x, 0f), Space.Self);
            Camera.main.transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(-touchDelta.y, 0f, 0f), Space.Self);

            //Limit Rotation
            /*
            Vector3 camEuler = Camera.main.transform.eulerAngles;
            Debug.Log(camEuler);
            Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, xRotationLowerLimit, xRotationUpperLimit), 0f, 0f);
            */
        }
        else
        {
            prevTouchPosition = Vector2.zero;
        }

        prevTouchCount = touchCount;
        prevTouchPosition = touchPosition;
    }
}
