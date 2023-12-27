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
    private float halfScreenWidth;
    private int rightFingerId = -1;
    private Vector2 prevTouchPosition = Vector2.zero;
    private Vector2 touchPosition = Vector2.zero;
    private float scaledRotationSpeed;
    private Vector3 move = Vector3.zero;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        halfScreenWidth = Screen.width / 2;
        scaledRotationSpeed = rotationSpeed / ((Screen.width) * 0.01f); //TODO Implement
        Debug.Log(scaledRotationSpeed);
    }

    private void Update()
    {
        Vector2 touchDelta;
       
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase) {
                case UnityEngine.TouchPhase.Began:
                    if (t.position.x > halfScreenWidth && rightFingerId==-1)
                    {
                        rightFingerId = t.fingerId;
                        prevTouchPosition = t.position;
                        touchPosition = t.position;
                    }
                    break;
                case UnityEngine.TouchPhase.Canceled:
                case UnityEngine.TouchPhase.Ended:
                    if(t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        prevTouchPosition = Vector2.zero;
                        touchPosition = Vector2.zero;
                    }
                    break;
                case UnityEngine.TouchPhase.Moved:
                    if (t.fingerId == rightFingerId)
                    {
                        touchPosition = t.position;
                        touchDelta = touchPosition - prevTouchPosition;

                        Debug.Log("Previous position: " + prevTouchPosition + "  Current position: " + touchPosition);
                        Debug.Log("Touch delta: " + touchDelta);

                        //Rotate the player object on the y axis and the camera on x axis to maintain independent rotation and not mess up with z axis
                        transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(0f, touchDelta.x, 0f), Space.Self);
                        Camera.main.transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(-touchDelta.y, 0f, 0f), Space.Self);

                        //Limit Rotation
                        Vector3 camEuler = Camera.main.transform.eulerAngles;
                        //Debug.Log(camEuler);
                        if (camEuler.x < 360 && camEuler.x >= 270) // Limit when looking up
                            Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, xRotationUpperLimit, 359.99f), camEuler.y, camEuler.z);
                        else // Limit when looking down
                            Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, 0f, xRotationLowerLimit), camEuler.y, camEuler.z);
                    }
                    break;
            }
        }

        prevTouchPosition = touchPosition;  // Set Prev touch position

        move = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical) * movementSpeed;
        body.velocity = move;
    }

    /*
    private void FixedUpdate()
    {
        body.MovePosition(transform.position + move * Time.fixedDeltaTime);
    }
    */
}
