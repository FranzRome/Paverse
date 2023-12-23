using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float movementSpeed = 10f;
    public float rotationSpeed = 50f;
    public float xRotationLowerLimit = -45, xRotationUpperLimit = 60;

    private Rigidbody body;
    private int prevTouchCount = 0;
    private Vector2 prevTouchPosition = Vector2.zero;
    private float scaledRotationSpeed;
    private Vector3 move = Vector3.zero;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        scaledRotationSpeed = rotationSpeed / ((Screen.width) * 0.01f); //TODO Implement
        Debug.Log(scaledRotationSpeed);
    }

    private void Update()
    {
        int touchCount = Input.touchCount;
        Vector2 touchPosition = Vector2.zero;
        Vector2 touchDelta = Vector2.zero;
        move = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical) * movementSpeed;

        if(touchCount > 0)
        {
            //Debug.Log("Touches > 0");

            touchPosition = Input.GetTouch(0).position;

            if (touchPosition.x > Screen.width * 0.4)
            {
                if (prevTouchCount == 0)
                {
                    prevTouchPosition = touchPosition;
                }

                touchDelta = touchPosition - prevTouchPosition;

                Debug.Log("Previous position: " + prevTouchPosition + "  Current position: " + touchPosition);
                Debug.Log("Touch delta: " + touchDelta);

                //Rotate the player object on the y axis and the camera on x axis to maintain independent rotation and not mess up with z axis
                transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(0f, touchDelta.x, 0f), Space.Self);
                Camera.main.transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(-touchDelta.y, 0f, 0f), Space.Self);

                //Limit Rotation

                Vector3 camEuler = Camera.main.transform.eulerAngles;
                //Debug.Log(camEuler);
                if (camEuler.x < 360 && camEuler.x >= 270)
                    Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, xRotationUpperLimit, 359.99f), camEuler.y, camEuler.z);
                else
                    Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, 0f, xRotationLowerLimit), camEuler.y, camEuler.z);
            }
        }
        else
        {
            prevTouchPosition = Vector2.zero;
        }

        prevTouchCount = touchCount;
        prevTouchPosition = touchPosition;
    }

    private void FixedUpdate()
    {
        body.MovePosition(transform.position + move * Time.fixedDeltaTime);
    }
}
