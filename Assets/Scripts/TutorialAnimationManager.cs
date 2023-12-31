using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWebManager : MonoBehaviour
{
#if !UNITY_ANDROID && !UNITY_IOS
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void HideTutorial()
    {
        anim.Play("Tutorial Web Hide");
    }
#endif
}
