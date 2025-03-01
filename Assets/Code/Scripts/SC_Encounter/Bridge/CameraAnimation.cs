using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{

    public void TriggerCameraSwitch()
    {
        GetComponent<Animator>().SetTrigger("Transition");
    }
}
