using UnityEngine.XR.ARFoundation;
using UnityEngine;
using TMPro;

public class Estimated_light : MonoBehaviour
{

    public ARCameraManager arcamman;
    public TMP_Text brightness;
    Light our_light;

    void OnEnable()
    {
        arcamman.frameReceived += getLight;
    }

    private void OnDisable()
    {
        arcamman.frameReceived -= getLight;
    }


    // Start is called before the first frame update
    void Start()
    {
        our_light = GetComponent<Light>();
    }

    void getLight(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.mainLightColor.HasValue)
        {
            //brightness.text = $"Color_value:{args.lightEstimation.mainLightColor.Value}";
            our_light.color = args.lightEstimation.mainLightColor.Value;
            //float average_brightness = 0.2126f * our_light.color.r + 0.7152f * our_light.color.g + 0.0722f * our_light.color.b;
            //brightness.text = "Intesità di luce: " +  average_brightness.ToString();
        }
    }
}
