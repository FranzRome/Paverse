using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Scene church;

    // Movement & Touch Inputs
    public FixedJoystick joystick;
    public float movementSpeed = 10f;
    public float rotationSpeed = 50f;
    public float xRotationLowerLimit = -45, xRotationUpperLimit = 60;
    private float halfScreenWidth;
    private int rightFingerId = -1;
    private Vector2 prevTouchPosition = Vector2.zero;
    private Vector2 touchPosition = Vector2.zero;
    private float scaledRotationSpeed;
    private Vector3 move = Vector3.zero;

    // Keyboard inputs
    private float horizontalValue, verticalValue;
    private float xRotation, yRotation;

    // Components
    private Rigidbody body;
    private new Animation animation;

    // Teleport destination
    private string destination;

    // Movement enabled
    private bool canMove;
    private bool launchScene;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; // Scene loaded event
        body = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();
        halfScreenWidth = Screen.width / 2;
        scaledRotationSpeed = rotationSpeed / ((Screen.width) * 0.01f); //TODO Implement
        //Debug.Log(scaledRotationSpeed);
        canMove = true;
        launchScene = false;

#if UNITY_ANDROID || UNITY_IOS
    Debug.Log("Mobile");
#endif

#if UNITY_WEBGL
       Debug.Log("Web");
        joystick.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }

    private void Update()
    {
        if (canMove)
        {
#if UNITY_ANDROID || UNITY_IOS
            Vector2 touchDelta;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);

                switch (t.phase)
                {
                    case UnityEngine.TouchPhase.Began:
                        if (t.position.x > halfScreenWidth && rightFingerId == -1)
                        {
                            rightFingerId = t.fingerId;
                            prevTouchPosition = t.position;
                            touchPosition = t.position;
                        }
                        break;
                    case UnityEngine.TouchPhase.Canceled:
                    case UnityEngine.TouchPhase.Ended:
                        if (t.fingerId == rightFingerId)
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
                        }
                        break;
                }
            }

            prevTouchPosition = touchPosition;  // Set Prev touch position

            move = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical + Vector3.up * body.velocity.y) * movementSpeed;
#endif

#if UNITY_WEBGL
        yRotation = Input.GetAxis("Mouse X");
        xRotation = -Input.GetAxis("Mouse Y");
            //Debug.Log(xRotation + " " + yRotation);
        float timeScaledRotationSpeed = rotationSpeed* Time.deltaTime;
            // Limit rotation delta on current frame
            yRotation = Mathf.Clamp(yRotation, -10, 10);
            xRotation = Mathf.Clamp(xRotation, -10, 10);

            transform.Rotate(80 * timeScaledRotationSpeed * new Vector3(0f, yRotation, 0f), Space.Self);
        Camera.main.transform.Rotate(80 * timeScaledRotationSpeed * new Vector3(xRotation, 0f, 0f), Space.Self);

        //Debug.Log(Input.GetAxis("Horizontal"));
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        move = (transform.right * horizontalValue + transform.forward * verticalValue) * movementSpeed;
        //Debug.Log(move);
#endif
            //Limit Rotation
            Vector3 camEuler = Camera.main.transform.eulerAngles;
            //Debug.Log(camEuler);
            if (camEuler.x < 360 && camEuler.x >= 270) // Limit when looking up
                Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, xRotationUpperLimit, 359.99f), camEuler.y, camEuler.z);
            else // Limit when looking down
                Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(camEuler.x, 0f, xRotationLowerLimit), camEuler.y, camEuler.z);

            body.velocity = move;
        }
    }

    public void BeginTeleport(string destination)
    {
        this.destination = destination;
        animation.Play();
    }

    public void Teleport()
    {
        print("Destination: " + destination);
        SceneManager.LoadSceneAsync(destination);
    }

    public void Load3dScene(string destination)
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.fullScreen = true;
        this.destination = destination;
        animation.Play();
        StartCoroutine(Load3DScene_Coroutine());

        //ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
        //ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;

    }

    public IEnumerator Load3DScene_Coroutine()
    {


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(destination);
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f && launchScene)
            {
                asyncOperation.allowSceneActivation = true;
                DisableLaunchScene();
            }
            yield return null;
        }
    }

    public void EnableLaunchScene()
    {
        launchScene = true;
    }

    public void DisableLaunchScene()
    {
        launchScene = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("Scene Loaded");
        SceneManager.SetActiveScene(scene);
        //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());


        if(SceneManager.GetActiveScene().name == "3D Church")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            //print("Players: " +  players.Length);
            foreach (GameObject p in players)
            {
                if (!p.Equals(this.gameObject))
                {
                    Destroy(p);
                }
            }
        }

        GameObject spawn = GameObject.Find("Spawn Point");
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        //SceneManager.UnloadSceneAsync("3D Tour");
    }

    private void EnableMovement()
    {
        this.canMove = true;
    }

    private void DisableMovement()
    {
        this.canMove = false;
    }
}
